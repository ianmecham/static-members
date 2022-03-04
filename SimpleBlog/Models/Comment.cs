using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBlog.Models
{
    public class Comment
    {
        public int ID { get; set; }

        public Post ParentPost { get; set; }
        public int PostID { get; set; }
        public string Content { get; set; }
        public DateTime DeletedOn { get; set; }
        
        public bool Deleted { get; set; }
    }
}
