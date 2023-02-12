using FilesManagerWithAzure.APP.Models;
using FilesManagerWithAzure.Core.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.IO;

namespace FilesManagerWithAzure.APP.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                string path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "UploadedFiles"));
                if (!Directory.Exists(path)) 
                    Directory.CreateDirectory(path); 
                using (var fileStream = new FileStream(Path.Combine(path, file.FileName), FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                return Ok();
            }
            return BadRequest();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}