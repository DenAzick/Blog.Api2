﻿using Blog.Api.Entities;

namespace Blog.Api.Models.BlogModels;

public class PostModel
{
    public Guid PostId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }

    public DateTime CreatedDate { get; set; } 
    public DateTime? UpdatedDate { get; set; }

    public Guid BlogId { get; set; }
    public virtual List<Like> Likes { get; set; }
    public virtual List<SavedPost> SavedPosts { get; set; }
}