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

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
        public IActionResult Index()
        {
            return View("Index",_userManager.Users.Where(u => u.UserName == User.Identity.Name).Single().PhotoUrl);
        }

        [HttpPost]
        public async Task<IActionResult> UploadProfilPicture(IFormFile file)
        {
            if (file.Length > 0)
            {
                Transformation transform = new Transformation();
                transform.Height(300);
                transform.Width(200);

                var uploadResult = await Cloudinary.UploadAsync(new ImageUploadParams()
                {
                    File = new FileDescription(file.FileName, file.OpenReadStream()),
                    Transformation= transform
                });

                string uri = uploadResult.SecureUri.AbsoluteUri;

                var user =  _userManager.Users.Where(u => u.UserName== User.Identity.Name).Single();
                user.PhotoUrl = uri;

                _questionManager.SetPhotoUrl(user, uri);

                return View("Index", uri);
            }

            return View("Index");
        }

    }
}
