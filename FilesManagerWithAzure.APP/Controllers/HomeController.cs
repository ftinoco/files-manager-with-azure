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
        public async Task<IActionResult> UploadFile(FileUploadDTO dto)
        {
            if (dto.File != null && dto.File.Length > 0)
            {
                string path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "UploadedFiles"));
                if (!Directory.Exists(path)) 
                    Directory.CreateDirectory(path); 
                using (var fileStream = new FileStream(Path.Combine(path, dto.File.FileName), FileMode.Create))
                {
                    await dto.File.CopyToAsync(fileStream);
                }
                 
                string[] files = System.IO.Directory.GetFiles(path, "*.*");

                foreach (string s in files)
                {
                    // Create the FileInfo object only when needed to ensure
                    // the information is as current as possible.
                    System.IO.FileInfo fi = null;
                    try
                    {
                        fi = new System.IO.FileInfo(s);
                    }
                    catch (System.IO.FileNotFoundException e)
                    {
                        // To inform the user and continue is
                        // sufficient for this demonstration.
                        // Your application may require different behavior.
                        Console.WriteLine(e.Message);
                        continue;
                    }
                    Console.WriteLine("{0} : {1}", fi.Name, fi.Directory);
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