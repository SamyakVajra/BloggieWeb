using bloggie.web.Models.Domain;
using Bloggie.Web.Data;
using Bloggie.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Bloggie.Web.Controllers
{
    public class AdminTagController : Controller

        //Constructor Injection
    {
        private readonly BloggieDbContext bloggieDbContext;

        public AdminTagController(BloggieDbContext bloggieDbContext)
        {
            this.bloggieDbContext = bloggieDbContext;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Add")]
        public IActionResult Add(SubmitTagRequest submitTagRequest)
        {
            var tag = new Tag
            {
                Name = submitTagRequest.Name,
                DisplayName = submitTagRequest.DisplayName,
            };

            bloggieDbContext.Tags.Add(tag);
            bloggieDbContext.SaveChanges();

            
            
            return View("Add");
        }

        [HttpGet]

        public IActionResult List() 
        {
            //Use dbContexxt to read the tags

            var tags = bloggieDbContext.Tags.ToList();
            return View(tags);
        }


    }

}
