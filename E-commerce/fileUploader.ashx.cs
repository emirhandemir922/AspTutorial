using System;
using System.Web;
using System.IO;

namespace AspTutorial
{
    public class fileUploader : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            try
            {
                string dirFullPath = HttpContext.Current.Server.MapPath("/img/products/");
                string[] files;
                int numFiles;
                files = System.IO.Directory.GetFiles(dirFullPath);
                numFiles = files.Length;
                numFiles = numFiles + 1;
                string str_image = "";

                foreach (string s in context.Request.Files)
                {
                    HttpPostedFile file = context.Request.Files[s];
                    string fileName = file.FileName;
                    string fileExtension = file.ContentType;

                    if (!string.IsNullOrEmpty(fileName))
                    {
                        fileExtension = Path.GetExtension(fileName);
                        str_image = fileName;
                        string pathToSave_100 = HttpContext.Current.Server.MapPath("/img/products/") + str_image;
                        file.SaveAs(pathToSave_100);
                    }
                }
            }
            catch (Exception ac)
            {
                context.Response.Write(ac);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

    }
}