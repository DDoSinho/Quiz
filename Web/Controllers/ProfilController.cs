using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Web.Controllers
{
    [Authorize]
    public class ProfilController : Controller
    {
        private Cloudinary Cloudinary;
        private IHostingEnvironment _environment;

        public ProfilController(IHostingEnvironment environment)
        {
            _environment = environment;

            Account cloudinaryAccount = new Account(
                "dlqdldxbw",
                "696585694432693",
                "fCyfZITBxypoZoyU_0Il5pL_uD8"
            );

            Cloudinary = new Cloudinary(cloudinaryAccount);
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    

        [HttpPost]
        public async Task<IActionResult> UploadProfilPicture(IFormFile file)
        {
            string uri = " ";

                if (file.Length > 0)
                {
                    var uploadResult = await Cloudinary.UploadAsync(new ImageUploadParams()
                    {
                        File = new FileDescription(file.FileName, file.OpenReadStream())
                    });

                var metadata = uploadResult.Metadata;
                foreach (var item in metadata)
                {
                    uri= uri+item.Value+" ";
                }

                }

            return Ok(uri+"asd");
        }

    }
}
