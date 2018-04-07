//using ImageService.Infrastructure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
//using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

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
            result = true;
            try
            {
                string extension = Path.GetExtension(path);
                if ((extension != ".png") && (extension != ".jpg") && (extension != ".bmp") && (extension != ".gif"))
                {
                    return "";
                }
                this.AddThumbnailImage(path);
                this.AddImage(path);
                return "";
            } catch (Exception e)
            {
                result = false;
                throw e;
            }
        }

        //retrieves the datetime WITHOUT loading the whole image
        private DateTime GetDateTakenFromImage(string imagePath)
        {
            Regex r = new Regex(":");
            using (FileStream fs = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
            using (Image myImage = Image.FromStream(fs, false, false))
            {
                PropertyItem propItem = myImage.GetPropertyItem(36867);
                string dateTaken = r.Replace(Encoding.UTF8.GetString(propItem.Value), "-", 2);
                return DateTime.Parse(dateTaken);
            }
        }
        private void AddImage(string path)
        {
            try
            {
                DateTime imageDate = GetDateTakenFromImage(path);
                string year = imageDate.Year.ToString();
                string month = imageDate.Month.ToString();

                string imagePath = Path.Combine(m_OutputFolder, year, month);
                Directory.CreateDirectory(imagePath);

                string fileName = Path.GetFileName(path);
                string finalPath = Path.Combine(imagePath, fileName);
                 
                for (int i = 1; i <= 100; i++)
                {
                    if (!Directory.Exists(finalPath))
                    {
                        break;
                    }
                    finalPath = Path.Combine(imagePath, fileName, "(", i.ToString(), ")");
                }
              
                File.Move(path, imagePath);
                
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        private void AddThumbnailImage(string path)
        {
            Image image = Image.FromFile(path);
            Image thumb = image.GetThumbnailImage(this.m_thumbnailSize, this.m_thumbnailSize, () => false, IntPtr.Zero);

            DateTime imageDate = GetDateTakenFromImage(path);
            string year = imageDate.Year.ToString();
            string month = imageDate.Month.ToString();

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
        }
    }
}