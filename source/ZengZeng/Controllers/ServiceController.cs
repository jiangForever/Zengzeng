using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ZengZeng.Models;

namespace ZengZeng.Controllers
{
    public class ServiceController : Controller
    {
        /// <summary>
        /// 生成二维码图片功能
        /// </summary>
        /// <param name="unique"></param>
        /// <returns></returns>
        public ActionResult QRImage(string unique)
        {
            string contentType = "";
            return File("", contentType);
        }


        /// <summary>
        /// 上传图片功能
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="folder"></param>
        /// <returns></returns>
        public ActionResult UpLoadBase64(string fileName, string folder)
        {
            string data = Request.Params["data"];
            ImageModel model = new ImageModel();
            try
            {
                string direct = Server.MapPath("/Image/");
                if (!Directory.Exists(direct + folder))
                {
                    Directory.CreateDirectory(direct + folder);
                }
                Guid guid = Guid.NewGuid();
                string extens = fileName.Split('.')[1]; //11.jpg
                string newFilePath = folder + "/" + guid + "." + extens;
                if (data != null)
                {
                    byte[] bytes = Convert.FromBase64String(data);
                    using (MemoryStream ms = new MemoryStream(bytes))
                    {
                        ms.Write(bytes, 0, bytes.Length);
                        var img = Image.FromStream(ms, true);
                        img.Save(direct + folder + "/" + guid + "." + extens);
                    }
                }
                else
                {
                    Request.Files[0].SaveAs(direct + folder + "/" + guid + "." + extens);
                }

                model.ImageUrl = newFilePath;
                model.IsSuccess = true;
            }
            catch (Exception ex)
            {
                model.IsSuccess = false;
            }
            Response.Headers.Add("Access-Control-Allow-Origin", "*");
            Response.Headers.Add("Access-Control-Allow-Methods", "POST");
            return Json(model);
        }
    }
}
