using System.Text.Json;

namespace BloggingAPI.Models.Entities
{
    public record BlogPost
    {
        public required int Id { get; set; }
        public required string Title { get; set; }
        public required string Content { get; set; }
        public required string Author { get; set; }
        public List<Comment>? Comments { get; set; }

        public bool IsValid() => !string.IsNullOrEmpty(Title) && !string.IsNullOrEmpty(Content) && !string.IsNullOrEmpty(Author);

        public ViewModels.BlogPost ToViewModel() => new()
        {
            Id = Id,
            Title = Title,
            Content = Content,
            Author = Author,
            Comments = Comments?.Select(c => c.ToViewModel()).ToList()
        };
    }
}
