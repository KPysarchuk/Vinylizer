using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vinylizer.Models;

namespace Vinylizer.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public void UploadAudioFile(HttpPostedFileBase file)
        {
            if (file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName).Replace(" ", string.Empty);
                DirectoryInfo dir = Directory.CreateDirectory("D:/Vinylizer/" + fileName);
                var path = Path.Combine(dir.FullName, fileName);
                file.SaveAs(path);

                var VinylizerFileName = new HttpCookie("VinylizerFileName", fileName);
                VinylizerFileName.Expires = DateTime.Now.AddYears(1);
                HttpContext.Response.Cookies.Add(VinylizerFileName);

            }
        }

        public ActionResult GetAudioFileForPlay(string fileName)
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes(string.Format("D:/Vinylizer/{0}/{1}", fileName, fileName));
            MemoryStream ms = new MemoryStream(fileBytes);

            return File(ms, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        public ActionResult GetFilterForPlay(int volumeLvl, string fileName)
        {
            string filerName = Converter.ChangeVolumeForTest(volumeLvl, fileName);
            byte[] fileBytes = System.IO.File.ReadAllBytes(string.Format("D:/Vinylizer/{0}/Test/{1}", fileName, filerName));
            MemoryStream  ms = new MemoryStream(fileBytes);

            return File(ms, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        public ActionResult GetAudioFileForDownload(string fileName, int volumeLvl)
        {
            Converter.ChangeVolume(volumeLvl, fileName);
            Converter.Merge(fileName);
            string mixName = string.Format("Converted{0}", fileName);
            byte[] fileBytes = System.IO.File.ReadAllBytes(string.Format("D:/Vinylizer/{0}/{1}", fileName, mixName));

            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, mixName);
        }
    }
}