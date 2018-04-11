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
        }

        public string AddFile(string path, out bool result)
        {
            Console.WriteLine("add again");
            result = true;
            try
            {
                Task t1 = new Task(() =>
                 {
                     while (!this.IsLocked(path))
                         Thread.Sleep(500);

                     string extension = Path.GetExtension(path);
                     if ((extension != ".png") && (extension != ".jpg") && (extension != ".bmp") && (extension != ".gif"))
                     {
                         Console.WriteLine("EXT. NOT MATCH");
                         Console.WriteLine(path + " File exs. is " + Path.GetExtension(path));
                         // return "Task succedded.";
                     }
                     Console.WriteLine("EXT. MATCH");
                  this.AddThumbnailImage(path);

                     this.AddImage(path);
                 });
                t1.Start();
                    //Thread.Sleep(1000);
               // });
               // t1.Start();
               // t1.Wait();

                /*Task t2 = new Task(() =>
                {*/
                    //Thread.Sleep(1000);
                //});
                //t2.Start();
                //t2.Wait();

                return "Task succedded.";
            }
            catch (Exception e)
            {
                Console.WriteLine("failed");
                result = false;
                throw e;
            }
        }

        //retrieves the datetime WITHOUT loading the whole image
        public DateTime GetDateTakenFromImage(string path)
        {
            /*Regex r = new Regex(":");
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            using (Image myImage = Image.FromStream(fs, false, false))
            {
                PropertyItem propItem = myImage.GetPropertyItem(36867);
                string dateTaken = r.Replace(Encoding.UTF8.GetString(propItem.Value), "-", 2);
                return DateTime.Parse(dateTaken);
            }*/

            string year = "";
            string month = "";
            try
            {
                var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
                var img = BitmapFrame.Create(fs);
                var metadata = img.Metadata as BitmapMetadata;
                DateTime datePicTaken = DateTime.Parse(metadata.DateTaken);
                year = datePicTaken.Year.ToString();
                month = datePicTaken.Month.ToString();
                Console.WriteLine($"Got Date from {path}, year={year} month={month}");
                return new DateTime(int.Parse(year), int.Parse(month), 1);
            }
            catch (Exception e)
            {
                Console.WriteLine("Couldnt resolve image date. path={0}\n exception={1}", path, e);
                return new DateTime(1, 1, 1);
            }

        }
        private void AddImage(string path)
        {
            string month, year;
            while (!IsLocked(path))
            {
                Console.Write("a");
                GC.Collect();
                Thread.Sleep(500);
            }
            string fileName = "", finalPath = "";
            try
            {
                Console.WriteLine("my path is" + path);
                DateTime imageDate = GetDateTakenFromImage(path);

               
                    year = imageDate.Year.ToString();
                    month = imageDate.Month.ToString();
                }
                catch (Exception)
                {
                    month = "undifined";
                    year = "undifined";
                }
            


                string imagePath = Path.Combine(m_OutputFolder, year, month);
                Directory.CreateDirectory(imagePath);
            Console.WriteLine("here (2) ");


            fileName = Path.GetFileName(path);
                 finalPath = Path.Combine(imagePath, fileName);

                for (int i = 1; i <= 100; i++)
                {
                    if (!Directory.Exists(finalPath))
                    {
                        break;
                    }
                    finalPath = Path.Combine(imagePath, fileName, "(", i.ToString(), ")");
                }

            Console.WriteLine("final path is " +  finalPath);
            while (!IsLocked(path))
            {
                Console.Write("a");
                GC.Collect();
                Thread.Sleep(500);
            }
            Console.WriteLine(path);
            Console.WriteLine(finalPath);
                File.Move(path, finalPath);
            
           
        }
        private void AddThumbnailImage(string path)
        {
            string year, month; 
            Image image = Image.FromFile(path);
            Image thumb = image.GetThumbnailImage(this.m_thumbnailSize, this.m_thumbnailSize, () => false, IntPtr.Zero);
            try
            {
                Console.WriteLine("my path is:" +  path);
                DateTime imageDate = GetDateTakenFromImage(path);
                 year = imageDate.Year.ToString();
                 month = imageDate.Month.ToString();

            } catch (Exception e)
            {
                month = "undifined";
                year = "undifined";
            }

            string thumbnailImagePath = Path.Combine(m_OutputFolder, "Thumbnails", year, month);
            Directory.CreateDirectory(thumbnailImagePath);

            string fileName = Path.GetFileName(path);
            string finalPath = Path.Combine(thumbnailImagePath, fileName);

            for (int i = 1; i <= 100; i++)
            {
                if (!Directory.Exists(finalPath))
                {
                    break;
                }
                finalPath = Path.Combine(thumbnailImagePath, fileName, "(", i.ToString(), ")");
            }

            thumb.Save(finalPath);
           // GC.Collect();
            image.Dispose();
            Thread.Sleep(500);
            //File.Delete(path);

            //thumb.Dispose()
        }

        private bool IsLocked(string strFullFileName)
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