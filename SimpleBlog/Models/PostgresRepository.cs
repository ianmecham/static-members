using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace SimpleBlog.Models
{
    public class PostgresRepository : IRepository
    {
        private readonly ApplicationDbContext context;

    public PostgresRepository(ApplicationDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Post>> GetPostListAsync () {
            return await EntityFrameworkQueryableExtensions.ToListAsync(context.Posts);
        }
        public async Task AddPostAsync(Post post)
        {
            post.PostedOn = DateTime.Now.ToLocalTime();
            context.Posts.Add(post);
            await context.SaveChangesAsync();
        }

        public async Task EditPostAsync(Post post)
        {
            post.EditedOn = DateTime.Now.ToLocalTime();
            context.Update(post);
            await context.SaveChangesAsync();
        }

        public async Task<Post> GetPostAsync(int postID)
        {
            return await context.Posts.Include(r => r.Comments)
                .Include(p => p.PostCategories) 
                .ThenInclude(pc => pc.Category)
                .FirstOrDefaultAsync(r => r.ID == postID);
        }

        public async Task<Category> GetCategoryAsync (int categoryID)
        {
            return await Task.Run(() => context.Categories.Include(c => c.PostCategories).ThenInclude(pc => pc.Post).First(r => r.CategoryId == categoryID));
        }

        public async Task AddCommentAsync(Comment comment)
        {
            context.Comments.Add(comment);
            await context.SaveChangesAsync();
        }

        public async Task DeleteCommentAsync(Comment comment)
        {
            comment.Deleted = true;
            comment.DeletedOn = DateTime.Now.ToUniversalTime();
            context.Update(comment);
            await context.SaveChangesAsync();
        }

        public async Task AddCategoryAsync (Category category, int postId)
        {

            var tempCat = await EntityFrameworkQueryableExtensions.FirstOrDefaultAsync(context.Categories, c => c.CategoryName == category.CategoryName);


           if(tempCat == null)
            {
                tempCat = new Category { CategoryName = category.CategoryName };
                context.Categories.Add(tempCat);
                await context.SaveChangesAsync();
            }

            var newPostCategory = new PostCategory()
            {
                CategoryId = tempCat.CategoryId,
                PostId = postId
            };

            context.PostCategories.Add(newPostCategory);
            await context.SaveChangesAsync();
            
        }


        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await context.Categories.Include(c => c.PostCategories).ThenInclude(r => r.Post).ThenInclude(r => r.Comments).ToListAsync();
        }

        public Task<Category> GetPostCategoriesAsync(int postID)
        {
            throw new NotImplementedException();
        }


        public async Task<Comment> GetCommentAsync(int commentID)
        {
            return await context.Comments.Include(r => r.ParentPost).FirstOrDefaultAsync(r => r.ID == commentID);
        }
    }
}
