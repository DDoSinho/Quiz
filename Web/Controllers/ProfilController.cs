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

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Web.Controllers
{
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
        public async Task<IActionResult> UploadProfilPicture(FileDescription files)
        {
            /*var upload = Path.Combine(_environment.WebRootPath, "uploads");
            using (var stream = new FileStream(Path.Combine(upload, files.FileName), FileMode.Create))
            {
                await files.CopyToAsync(stream);

                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(files.FileName, stream)
                };
                var uploadResult = await Cloudinary.UploadAsync(uploadParams);
                Console.WriteLine();
            }*/

            return View();
        }

    }
}
