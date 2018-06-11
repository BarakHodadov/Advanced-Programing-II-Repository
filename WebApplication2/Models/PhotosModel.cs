using ImageService.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class PhotosModel
    {
        WebClient client;
        public string[] Images { get; set; }
        public string Path { get; set; }

        public PhotosModel(string path = "")
        {
            if (!path.Equals(""))
            {
                this.Path = path;
                this.Images = Directory.GetFiles(this.Path, "*", SearchOption.AllDirectories);
                return;
            }
            client = WebClient.Instance;
            string config = client.sendrecieve(client.makeData(CommandEnum.GetConfigCommand));
            this.Path = config.Split('#')[1];
            this.Images = Directory.GetFiles(this.Path, "*", SearchOption.AllDirectories);
        }

        public List<string> GetThumbnailImages()
        {
            string[] paths = Directory.GetFiles(System.IO.Path.Combine(this.Path,"Thumbnails"), "*", SearchOption.AllDirectories);
            List<string> relativePaths = new List<string>();
            foreach(string str in paths)
            {
                relativePaths.Add(ToRelativePath(str));
            }
            return relativePaths;
        }


        /// <summary>
        /// convert absolout path to relative path
        /// </summary>
        public string ToRelativePath(string path)
        {
            return path.Replace(HttpContext.Current.Server.MapPath("~/"), "~/").Replace(@"\", "/");
        }


        public void DeletePhoto(string relative_path)
        {
            File.Delete(System.IO.Path.GetFullPath(relative_path));

            string absolute_path =HttpContext.Current.Server.MapPath(relative_path);
            File.Delete(absolute_path);

        }
    }
}