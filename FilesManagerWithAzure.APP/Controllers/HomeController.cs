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
        private readonly IBlobInfoService _blobInfoService;

        public HomeController(ILogger<HomeController> logger, IBlobService blobService, IBlobInfoService blobInfoService)
        {
            _logger = logger;
            _blobService = blobService;
            _blobInfoService = blobInfoService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadFile(FileUploadDTO dto)
        {
            try
            {
            if (dto.File != null && dto.File.Length > 0)
            {
                using var ms = new MemoryStream();
                await dto.File.CopyToAsync(ms);
                ms.Seek(0, SeekOrigin.Begin);
                var response = await _blobService.UploadFileBlob(ms, dto.File.FileName);

                // saving metadata at cosmosdb
                await _blobInfoService.CreateBlobInfo(new FileDTO
                {
                    Description = dto.Description,
                    Extension = dto.File.ContentType,
                    FileName = dto.File.FileName,
                    CreationDate = response.Value.LastModified.DateTime,
                    LastModificationDate = response.Value.LastModified.DateTime
                });

                return Ok();
                }

            }
            catch (Exception e)
            { 
                throw;
            }
            return BadRequest();
        }

        public async Task<IActionResult> Files()
        {
            List<FileDTO> files = new();
            var result = await _blobInfoService.GetAllBlobInfo();
            foreach (var file in result)
            { 
                files.Add(new FileDTO
                {
                    CreationDate = file.CreationDate,
                    Extension = file.ContentType,
                    FileName = file.FileName, 
                    Description = file.Description,
                    LastModificationDate = file.LastModificationDate
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