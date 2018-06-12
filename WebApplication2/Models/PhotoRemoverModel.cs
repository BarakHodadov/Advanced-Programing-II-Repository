using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class PhotoRemoverModel
    {
        public string PhotoToRemove { get; set; }

        public PhotoRemoverModel(string path)
        {
            this.PhotoToRemove = path;
        }

        public void remove()
        {

        }

        public string ToRelativePath(string AbsolutepPath)
        {
            string rel_path = AbsolutepPath.Replace(HttpContext.Current.Server.MapPath("~/"), "~/").Replace(@"\", "/");
            return rel_path;
        }
    }
}