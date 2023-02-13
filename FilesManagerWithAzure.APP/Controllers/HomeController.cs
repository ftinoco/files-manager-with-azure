using FilesManagerWithAzure.APP.Models;
using FilesManagerWithAzure.Core.DTOs;
using FilesManagerWithAzure.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FilesManagerWithAzure.APP.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBlobService _blobService;

        public HomeController(ILogger<HomeController> logger, IBlobService blobService)
        {
            _logger = logger;
            _blobService = blobService;
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
                using var ms = new MemoryStream();
                await dto.File.CopyToAsync(ms);
                ms.Seek(0, SeekOrigin.Begin);
                await _blobService.UploadFileBlob(ms, dto.File.FileName);

                return Ok();
            }
            return BadRequest();
        }

        public async Task<IActionResult> Files()
        {
            List<FileDTO> files = new();
            var filesName = await _blobService.GetAllBlobs();
            foreach (string s in filesName)
            {
                FileInfo fi;
                try
                {
                    fi = new FileInfo(s);
                }
                catch (FileNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }
                files.Add(new FileDTO
                {
                    CreationDate = fi.CreationTime,
                    Extension = fi.Extension,
                    FileName = fi.Name,
                    LastAccessDate = fi.LastAccessTime,
                    LastModificationDate = fi.LastWriteTime
                });
            }
            return View(files);
        }


        [HttpGet]
        public async Task<FileResult> GetFile(string fileName)
        {
            var result = await _blobService.GetBlobByName(fileName);
            return File(result.Blob.ToArray(), result.ContentType, fileName);
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}