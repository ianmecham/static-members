using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBlog.Models
{
    public interface IRepository
    {
        public Task<IEnumerable<Post>> GetPostListAsync();

        public Task AddPostAsync(Post post);
        public Task<Post> GetPostAsync(int postID);

        public Task<Category> GetCategoryAsync(int categoryID);

        public Task EditPostAsync(Post post);

        public Task AddCommentAsync(Comment comment);

        public Task DeleteCommentAsync(Comment comment);

        public Task<Comment> GetCommentAsync(int commnetID);

        public Task AddCategoryAsync(Category Category, int PostId);

        public Task<IEnumerable<Category>> GetCategoriesAsync();

        public Task<Category> GetPostCategoriesAsync(int postID);
    }
}
 