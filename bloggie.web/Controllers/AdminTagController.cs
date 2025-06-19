using bloggie.web.Models.Domain;
using Bloggie.Web.Data;
using Bloggie.Web.Models.ViewModels;
using Bloggie.Web.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace Bloggie.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminTagController : Controller

    //Constructor Injection
    {
        private readonly ITagRepository tagRepository;

        public AdminTagController(ITagRepository tagRepository)
        {
            this.tagRepository = tagRepository;
        }



        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Add")]
        public async Task<IActionResult> Add(SubmitTagRequest submitTagRequest)
        {
            var tag = new Tag
            {
                Name = submitTagRequest.Name,
                DisplayName = submitTagRequest.DisplayName,
            };


            await tagRepository.AddAsync(tag);


            return RedirectToAction("List");
        }

        [HttpGet]

        public async Task<IActionResult> List()
        {
            //Use dbContexxt to read the tags

            var tags = await tagRepository.GetAllAsync();
            return View(tags);
        }

        [HttpGet]

        public async Task<IActionResult> Edit(Guid id)
        {
            var tag = await tagRepository.GetAsync(id);

            if (tag != null)
            {
                var editTagRequest = new EditTagRequest
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName
                };

                return View(editTagRequest);
            }

            return View(null);
        }

        [HttpPost]

        public async Task<IActionResult> Edit(EditTagRequest editTagRequest)
        {
            var tag = new Tag
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName
            };

            var updatedTag = await tagRepository.UpdateAsync(tag);

            if (updatedTag != null)
            {
                //Show success notification
                return RedirectToAction("List");
            }

            //Show failure Notification
            return RedirectToAction("Edit", new { id = editTagRequest.Id });

        }

        //[HttpGet]

        //public async Task<IActionResult> Delete(Guid id)
        //{
        //    var tag = await bloggieDbContext.Tags.FindAsync(id);
        //    if (tag != null)
        //    {
        //        bloggieDbContext.Tags.Remove(tag);
        //       await bloggieDbContext.SaveChangesAsync();
        //        //Show success notification
        //        return RedirectToAction("List");
        //    }
        //    //Show failure Notification
        //    return RedirectToAction("Edit", new { id = id });
        //}

        [HttpPost]
        public async Task<IActionResult> Delete(EditTagRequest editTagRequest)
        {
            var deletedTag = await tagRepository.DeleteAsync(editTagRequest.Id);

            if (deletedTag != null)
            {
                //Show success notificationg
                return RedirectToAction("List");
            }


            //Show failure Notification
            return RedirectToAction("Edit", new { id = editTagRequest.Id });

        }



    }

}
