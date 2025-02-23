namespace BloggingAPI.Models.ViewModels
{
    public record BlogPost
    {
        public int? Id { get; set; }
        public required string Title { get; set; }
        public required string Content { get; set; }
        public required string Author { get; set; }
        public List<Comment>? Comments { get; set; }

        public bool IsValid() => !string.IsNullOrEmpty(Title) && !string.IsNullOrEmpty(Content) && !string.IsNullOrEmpty(Author);

        public Entities.BlogPost ToEntity() => new()
        {
            Id = 0,
            Title = Title,
            Content = Content,
            Author = Author
        };
    }
}
