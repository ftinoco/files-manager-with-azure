﻿using FilesManagerWithAzure.APP.Models;
using FilesManagerWithAzure.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using FilesManagerWithAzure.Core.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.IO;
using System.Net.Mime;

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
                string path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "UploadedFiles"));
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                using (var fileStream = new FileStream(Path.Combine(path, dto.File.FileName), FileMode.Create))
                {
                    await dto.File.CopyToAsync(fileStream);
                }
                return Ok();
            }
            return BadRequest();
        }

        public IActionResult Files()
        {
            List<FileDTO> files = new();
            string path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "UploadedFiles"));
            string[] filesName = System.IO.Directory.GetFiles(path, "*.*");

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
        public FileResult GetFile(string name)
        {
            try
            {
                string path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "UploadedFiles"));
                string fullPath = Path.Combine(path, name);
                var stream = new FileStream(fullPath, FileMode.Open);

                return File(stream, MediaTypeNames.Application.Octet, name);
            }
            catch (Exception e)
            {

                throw e;
            }
        

       
        }
        public async Task<IActionResult> GetImage()
        {
            var result = await _blobService.GetBlobByName("Screenshot_2023-01-23-06-23-36-352_com.yzbdz.shuma.jpg");
            return File(result.Blob.ToArray(), result.ContentType);
        }
        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}