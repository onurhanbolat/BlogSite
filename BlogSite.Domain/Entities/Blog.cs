using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BlogSite.Domain.Entities
{
    public class Blog
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string? Summary { get; set; }
        public string Content { get; set; }

        [ValidateNever]
        public string AuthorId { get; set; }

        [ValidateNever] 
        public DateTime CreatedDate { get; set; }

        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public int ViewCount { get; set; } = 0;

    }
}
