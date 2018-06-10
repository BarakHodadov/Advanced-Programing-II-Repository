using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.IO;

namespace WebApplication2.Models
{
    public class ImageWebModel
    {
        WebClient client;
        public IEnumerable<Student> Students { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Service status")]
        public string Status { get; set; }

        [Required]
        [Display(Name = "Number of photos")]
        public int NumPhotos
        {
            get
            {
                string[] config = this.client.sendrecieve(this.client.makeData(ImageService.Infrastructure.CommandEnum.GetConfigCommand)).Split('#');
                string path = config[1];
                return this.NumberOfImages(path);
            }
        }

        public Student StudentInfo { get; set; }

        public ImageWebModel(List<Student> s)
        {
            this.Students = s;
            this.Status = "activated";
            client = WebClient.Instance;
        }


        public int NumberOfImages(string path)
        {
            /* if path is relative
            string myDir = HttpContext.Current.Server.MapPath(path);
            int filesNum = Directory.GetFiles(myDir, "*", SearchOption.AllDirectories).Length;
            return filesNum;
            */
            
            //if path is absulute
            int filesNum = Directory.GetFiles(path, "*", SearchOption.AllDirectories).Length;
            return filesNum;
        }



    }
}