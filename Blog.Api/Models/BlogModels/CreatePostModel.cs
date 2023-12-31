﻿namespace Blog.Api.Models.BlogModels;

public class CreatePostModel
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime CreatedDate { get; set; } 
    public DateTime UpdatedDate { get; set; }

    public Guid BlogId { get; set; }
}