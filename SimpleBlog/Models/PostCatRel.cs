using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBlog.Models
{
    public class PostCatRel
    {
        public IEnumerable<Post> posts { get; set; }
        public Category category { get; set; }
    }
}
