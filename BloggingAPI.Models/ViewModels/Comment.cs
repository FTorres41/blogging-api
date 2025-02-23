namespace BloggingAPI.Models.ViewModels
{
    public record Comment
    {
        public required string Author { get; set; }
        public required string Content { get; set; }

        public bool IsValid() => !string.IsNullOrEmpty(Author) && !string.IsNullOrEmpty(Content);

        public Entities.Comment ToEntity(int blogPostId) => new()
        {
            Id = 0,
            Author = Author,
            Content = Content,
            BlogPostId = blogPostId
        };
    }
}
