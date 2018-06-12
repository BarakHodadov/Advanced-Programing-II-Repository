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
                this.Path = ToRelativePath(path);
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
        public string ToRelativePath(string AbsolutepPath)
        {
            return AbsolutepPath.Replace(HttpContext.Current.Server.MapPath("~/"), "~/").Replace(@"\", "/");
        }

        public string GetFullImagePath(string path)
        {
            return System.IO.Path.Combine(this.Path,path.Replace(@"/Images/Thumbnails/", @"/").Replace(@"/",@"\")).Replace(@"~\","");
        }

        public string getImageName(string path)
        {
            return System.IO.Path.GetFileName(path);
        }
    }
}