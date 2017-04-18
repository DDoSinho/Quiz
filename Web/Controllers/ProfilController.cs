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
using Microsoft.AspNetCore.Identity;
using Dal.Model.Identity;
using Dal;

namespace Web.Controllers
{
    [Authorize]
    public class ProfilController : Controller
    {
        private Cloudinary Cloudinary;
        private UserManager<QuizUser> _userManager;
        private QuestionManager _questionManager;

        public ProfilController(UserManager<QuizUser> userManager, QuestionManager questionManager)
        {
            _userManager = userManager;
            _questionManager = questionManager;

            Account cloudinaryAccount = new Account(
                "dlqdldxbw",
                "696585694432693",
                "fCyfZITBxypoZoyU_0Il5pL_uD8"
            );

            Cloudinary = new Cloudinary(cloudinaryAccount);
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View("Index", (await _userManager.GetUserAsync(User)).PhotoUrl);
        }

        [HttpPost]
        public async Task<IActionResult> UploadProfilPicture(IFormFile file)
        {
            if (file.Length > 0)
            {
                Transformation transform = new Transformation();
                transform.Height(400);
                transform.Width(400);

                var uploadResult = await Cloudinary.UploadAsync(new ImageUploadParams()
                {
                    File = new FileDescription(file.FileName, file.OpenReadStream()),
                    Transformation= transform
                });

                string uri = uploadResult.SecureUri.AbsoluteUri;

                var user = await _userManager.GetUserAsync(User);
                user.PhotoUrl = uri;

                _questionManager.SetPhotoUrl(user, uri);

                return View("Index", uri);
            }

            return View("Index");
        }

    }
}
