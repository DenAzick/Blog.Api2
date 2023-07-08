namespace Blog.Api.Models.BlogModels;

public class CreateBlogModel
{
    public required string Name { get; set; }
    public IFormFile? Media { get; set; }
    public string? Description { get; set; }
    public Guid UserId { get; set; }
}