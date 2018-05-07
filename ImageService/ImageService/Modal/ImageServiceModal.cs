using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;


namespace ImageService.Modal
{
    public class ImageServiceModal : IImageServiceModal
    {
        #region Members
        private string m_OutputFolder;            // The Output Folder
        private int m_thumbnailSize;              // The Size Of The Thumbnail Size
        #endregion

        public ImageServiceModal(string outputFolder, int thumbnailSize)
        {
            this.m_OutputFolder = outputFolder;
            this.m_thumbnailSize = thumbnailSize;

            DirectoryInfo di = Directory.CreateDirectory(outputFolder);
            di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
        }

        public string AddFile(string path, out bool result)
        {
            result = true;
            try
            {
                Task t1 = new Task(() =>
                 {
                     while (!this.IsAvailable(path))
                         Thread.Sleep(500);

                     string extension = Path.GetExtension(path);
                     if ((extension != ".png") && (extension != ".jpg") && (extension != ".bmp") && (extension != ".gif"))
                     {
                         //Console.WriteLine("EXT. NOT MATCH");
                         //Console.WriteLine(path + " File exs. is " + Path.GetExtension(path));
                         // return "Task succedded.";
                     }
                     this.AddThumbnailImage(path);

                     this.AddImage(path);
                 });
                t1.Start();

                return "Task succedded.";
            }
            catch (Exception e)
            {
                //Console.WriteLine("failed");
                result = false;
                throw e;
            }
        }

        //retrieves the datetime WITHOUT loading the whole image
        public DateTime GetDateTakenFromImage(string path)
        {
            //set a default year and month
            string year = "";
            string month = "";
            try
            {
                var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
                var image = BitmapFrame.Create(fileStream);
                var metadata = image.Metadata as BitmapMetadata;
                DateTime dateTaken = DateTime.Parse(metadata.DateTaken);

                year = dateTaken.Year.ToString();
                month = dateTaken.Month.ToString();
                return new DateTime(int.Parse(year), int.Parse(month), 1);
            }
            catch (Exception )
            {
                // if there was an error than set a default date.
                return new DateTime(1, 1, 1);
            }
        }
        private void AddImage(string path)
        {
            string month, year, ext;
            while (!IsAvailable(path)) // wait for the image's resources to be released
            {
                GC.Collect();
                Thread.Sleep(500);
            }
            string fileName = "", finalPath = ""; // default fileName and path
            try
            {
                // get the date from the image
                DateTime imageDate = GetDateTakenFromImage(path);
                year = imageDate.Year.ToString();
                month = imageDate.Month.ToString();
            }
            catch (Exception)
            {
                // if there was an error set to undifined
                month = "undifined";
                year = "undifined";
            }
            
            string imagePath = Path.Combine(m_OutputFolder, year, month);
            Directory.CreateDirectory(imagePath);

            fileName = Path.GetFileNameWithoutExtension(path);
            ext = Path.GetExtension(path);
            finalPath = Path.Combine(imagePath, fileName + ext); // concatenate the path, name and extention

            // if there is an image with this name add a number to the name
            for (int i = 1; i <= 100; i++)
            {
                if (!File.Exists(finalPath))
                {
                    break;
                }
                finalPath = Path.Combine(imagePath, fileName + "(" + i.ToString() + ")" + ext);
            }
            
            // wait until the image if free to use
            while (!IsAvailable(path))
            {
                GC.Collect();
                Thread.Sleep(500);
            }
            // move the image to it's new location
            File.Move(path, finalPath);


        }
        private void AddThumbnailImage(string path)
        {
            string year, month;
            Image image = Image.FromFile(path);
            Image thumb = image.GetThumbnailImage(this.m_thumbnailSize, this.m_thumbnailSize, () => false, IntPtr.Zero);
            try
            {
                // get the date from the image
                DateTime imageDate = GetDateTakenFromImage(path);
                year = imageDate.Year.ToString();
                month = imageDate.Month.ToString();

            }
            catch (Exception)
            {
                // if there was an error set a default values
                month = "undifined";
                year = "undifined";
            }

            // create the dir
            string thumbnailImagePath = Path.Combine(m_OutputFolder, "Thumbnails", year, month);
            Directory.CreateDirectory(thumbnailImagePath);

            string fileName = Path.GetFileNameWithoutExtension(path);
            string ext = Path.GetExtension(path);
            string finalPath = Path.Combine(thumbnailImagePath, fileName + ext);

            // if there is an image with this name add a number to the name
            for (int i = 1; i <= 100; i++)
            {
                if (!File.Exists(finalPath))
                {
                    break;
                }
                finalPath = Path.Combine(thumbnailImagePath, fileName + "(" + i.ToString() + ")" + ext);
            }

            thumb.Save(finalPath);
            image.Dispose();
            Thread.Sleep(500);
        }

        private bool IsAvailable(string strFullFileName)
        {
            /*bool blnReturn = false;
            FileStream fs;
            try
            {
                fs = File.Open(strFullFileName, FileMode.OpenOrCreate, FileAccess.Read, FileShare.None);
                fs.Close();
            }
            catch (IOException ex)
            {
                blnReturn = true;
            }
            return blnReturn;*/

            // If the file can be opened for exclusive access it means that the file
            // is no longer locked by another process.

            // If the file can be opened for exclusive access it means that the file
            // is no longer locked by another process.
            try
            {
                using (FileStream inputStream = File.Open(strFullFileName, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    if (inputStream.Length > 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}