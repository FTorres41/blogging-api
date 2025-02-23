namespace BloggingAPI.Models.Entities
{
    public record Comment
    {
        public required int Id { get; set; }
        public required int BlogPostId { get; set; }
        public required string Author { get; set; }
        public required string Content { get; set; }

        public bool IsValid() => !string.IsNullOrEmpty(Author) && !string.IsNullOrEmpty(Content);

        public ViewModels.Comment ToViewModel() => new()
        {
            Author = Author,
            Content = Content
        };
    }
}
