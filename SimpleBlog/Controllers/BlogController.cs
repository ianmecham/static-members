using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SimpleBlog.Models;

namespace SimpleBlog.Controllers
{
    public class BlogController : Controller
    {
        private readonly ILogger<BlogController> _logger;

        private IRepository _repository;

        public IRepository PostgresRepository => _repository;

        public BlogController(ILogger<BlogController> logger, IRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<IActionResult> Posts()
        {
            return View(await _repository.GetPostListAsync());
        }
        public async Task<IActionResult> Categories()
        {
            return View(await _repository.GetCategoriesAsync());
        }

        public async Task<IActionResult> PostCategory(int ID)
        {
            var category = await _repository.GetCategoryAsync(ID);

            return View(category);
        }


        public async Task<IActionResult> Detail(int ID)
        {
            var post = await _repository.GetPostAsync(ID);
            return View(post);
        }

        [HttpGet]
        public IActionResult NewPost()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> NewPost(Post post)
        {
            await _repository.AddPostAsync(post);
            return View("Posts", await _repository.GetPostListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int ID)
        {

            var post = await _repository.GetPostAsync(ID);
            return View(post);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(Post post)
        {
            await Task.Run(() => _repository.EditPostAsync(post));
            return RedirectToAction("Detail", new { ID = post.ID });
        }

        [HttpGet]
        public IActionResult NewComment(int ID)
        {
            return View(ID);
        }

        [HttpPost]
        public async Task<IActionResult> NewComment(Comment comment)
        {
            await _repository.AddCommentAsync(comment);
            return RedirectToAction("Detail", new { ID = comment.PostID });
        }

        public async Task<IActionResult> DeleteComment(int commentId)
        {
            Comment comment = await _repository.GetCommentAsync(commentId);
            await _repository.DeleteCommentAsync(comment);
            return RedirectToAction("Detail", new { ID = comment.PostID });
        }


        public async Task <IActionResult> AddCategory(Category category, int PostId)
        {
            
            await _repository.AddCategoryAsync(category, PostId);
            return RedirectToAction("Detail", new { ID = PostId });

        }

    }
}
