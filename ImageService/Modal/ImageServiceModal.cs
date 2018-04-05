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

        public ImageServiceModal(string outputFolder, int thumnailSize)
        {
            this.m_OutputFolder = outputFolder;
            this.m_thumbnailSize = thumnailSize;
        }

        public string AddFile(string path, out bool result)
        {
            try
            {
                DateTime imageDate = GetDateTakenFromImage(path);
                string year = imageDate.Year.ToString();
                string month = imageDate.Month.ToString();

                string imagePath = Path.Combine(m_OutputFolder, year, month);
                Directory.CreateDirectory(imagePath);

                string fileName = Path.GetFileName(path);
                imagePath = Path.Combine(imagePath, fileName);
                
                File.Move(path, imagePath);

                result = true;
                return "";
            } catch (Exception e)
            {
                result = false;
                return "Error";
            }
        }

        

        //retrieves the datetime WITHOUT loading the whole image
        public DateTime GetDateTakenFromImage(string imagePath)
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
    }
}