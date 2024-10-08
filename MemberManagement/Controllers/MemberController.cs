using StudioManagement.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using StudioManagement.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Drawing;
using System.Data;
using Syncfusion.Pdf;
using Syncfusion.HtmlConverter;


namespace StudioManagement.Controllers
{
    public class MemberController : Controller
    {
        
        private readonly ILogger<MemberController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly MyDbContext _db;
        public MemberController(ILogger<MemberController> logger, IWebHostEnvironment webHostEnvironment, MyDbContext db)
        {
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
            _db = db;
        }
        public IActionResult MemberIndex(string strSearch, int pg=1, int? StudioID=0)
        {
           
            return View();
        }
        
        public IActionResult Create(int? StudioID, Member member)
        {
            
            return View();
        }   
        
        [HttpPost]
        public IActionResult Create(Member member, IFormFile? file)
        {
                return View();
            
        }

        public IActionResult Edit(int? MemberId) 
        {
           
            return View();
        }

        [HttpPost]
        public IActionResult Edit(Member member, IFormFile? file)
        {
         
                return View();
        }
        public IActionResult Delete(int? MemberId)
        {
            return View();
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? MemberId)
        {
            return RedirectToAction("MemberIndex");
        }
        
        public ActionResult Get(int? MemberId)
        {
            return View();
        }

        public ActionResult GetMem(int? MemberId)
        {
            return View();
        }

        [HttpGet]
        public ActionResult ExportToPDF(int MemberId)
        {
            //Initialize HTML to PDF converter.
            HtmlToPdfConverter htmlConverter = new HtmlToPdfConverter();
            BlinkConverterSettings blinkConverterSettings = new BlinkConverterSettings();
            ////Set Blink viewport size.
            blinkConverterSettings.ViewPortSize = new Syncfusion.Drawing.Size(800, 0);
            //Assign Blink converter settings to HTML converter.
            htmlConverter.ConverterSettings = blinkConverterSettings;
            //Convert URL to PDF document.
            PdfDocument document = htmlConverter.Convert($"https://localhost:7056/Member/GetMem?MemberId={MemberId}");
            //Create memory stream.
            MemoryStream stream = new MemoryStream();
            //Save the document to memory stream.
            document.Save(stream);
            return File(stream.ToArray(), System.Net.Mime.MediaTypeNames.Application.Pdf, "MemberInfo.pdf");
        }    
    }
}
