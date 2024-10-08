using StudioManagement.Data;
using StudioManagement.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using System.Diagnostics.Metrics;
using Syncfusion.Pdf;
using Syncfusion.HtmlConverter;

namespace StudioManagement.Controllers
{
    public class HomeController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager
            .GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            logger.LogWarning("This is a MEL warning on the privacy page");
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            log.Info("About page privacy.");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Studio stu, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string studioPath = Path.Combine(wwwRootPath, @"images\studio");

                    using (var fileStream = new FileStream(Path.Combine(studioPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    stu.StudioPic = @"\images\studio\" + fileName;
                }

                TempData["success"] = "New Studio created successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Edit(int? StudioID)
        {
      
            return View();
        }
        [HttpPost]
        public IActionResult Edit(Studio stu, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string studioPath = Path.Combine(wwwRootPath, @"images\studio");

                    if (!string.IsNullOrEmpty(stu.StudioPic))
                    {
                        //delete ola image
                        var oldImagePath =
                            Path.Combine(wwwRootPath, stu.StudioPic.TrimStart('\\'));

                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(studioPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    stu.StudioPic = @"\images\studio\" + fileName;
                }

                TempData["success"] = "Studio updated successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Delete(int? StudioID)
        {
           
            return View();
        }
        //[Route("studio/Delete")]
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? StudioID)
        {
            return RedirectToAction("Index");
        }

        public ActionResult Get(int? StudioID)
        {
   
            return View();
        }
        public ActionResult GetStudio(int? StudioID)
        {
     
            return View();
        }

        [HttpGet]
        public ActionResult ExportToPDF(int StudioID)
        {
            //Initialize HTML to PDF converter.
            HtmlToPdfConverter htmlConverter = new HtmlToPdfConverter();
            BlinkConverterSettings blinkConverterSettings = new BlinkConverterSettings();
            ////Set Blink viewport size.
            blinkConverterSettings.ViewPortSize = new Syncfusion.Drawing.Size(800, 0);
            //Assign Blink converter settings to HTML converter.
            htmlConverter.ConverterSettings = blinkConverterSettings;
            //Convert URL to PDF document.
            PdfDocument document = htmlConverter.Convert($"https://localhost:7056/Home/GetStudio?StudioID={StudioID}");
            //Create memory stream.
            MemoryStream stream = new MemoryStream();
            //Save the document to memory stream.
            document.Save(stream);
            return File(stream.ToArray(), System.Net.Mime.MediaTypeNames.Application.Pdf, "StudioInfo.pdf");
        }
    }
}
