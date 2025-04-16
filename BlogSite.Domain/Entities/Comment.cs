using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSite.Domain.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public int BlogId { get; set; }
        public string AuthorId { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }

        public Blog Blog { get; set; }
    }

}
