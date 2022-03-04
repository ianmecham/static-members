using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using SimpleBlog.Models;


namespace Blog.Pages
{
    public class CategoryPageModel : PageModel {

        //get functions from repo
        public CategoryPageModel (IRepository repository) {
            this.repository = repository;
        }
        private IRepository repository;

        //get data to be used inside of page
        //[BindProperty]
        [BindProperty (SupportsGet = true)]
        public string CategoryName {get; set;}
        public string TheGuy {get; set;}
        public IEnumerable<Post> Posts {get; set;}
        

        //code to run when receiving a get request from user
        public async Task OnGet (int id) {
            var category = await repository.GetCategoryAsync(id);
            CategoryName = category.CategoryName;
            Posts = category.PostCategories.Select(pc => pc.Post);
        }





    }
}
