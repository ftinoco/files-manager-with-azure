using FilesManagerWithAzure.APP.Models;
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

        public async Task<IActionResult> GetImage()
        {
            var result = await _blobService.GetBlobByName("Screenshot_2023-01-23-06-23-36-352_com.yzbdz.shuma.jpg");
            return File(result.Blob.ToArray(), result.ContentType);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}