﻿using Blog.Api.Managers.BlogManager;
using Blog.Api.Models.BlogModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class BlogsController : ControllerBase
{
    private readonly BlogManager _blogManager;

    public BlogsController(BlogManager blogManager)
    {
        _blogManager = blogManager;
    }

    [HttpGet]
    public async Task<IActionResult> GetBlogs()
    {
        return Ok(await _blogManager.GetBlogs());
    }

    [HttpGet("blogId")]
    public async Task<IActionResult> GetBlogById(Guid blogId)
    {
        return Ok(await _blogManager.GetBlogById(blogId));
    }

    [HttpGet("userId")]
    public async Task<IActionResult> GetBlogByAuthor(Guid userId)
    {
        return Ok(await _blogManager.GetBlogByAuthor(userId));
    }

    [HttpGet("userName")]
    public async Task<IActionResult> GetBlogByAuthor(string userName)
    {
        return Ok(await _blogManager.GetBlogByAuthor(userName));
    }

    [HttpPost]
    public async Task<IActionResult> CreateBlog(CreateBlogModel model)
    {
        return Ok(await _blogManager.CreateBlog(model));
    }

    [HttpPut]
    public async Task<IActionResult> UpdateBlog(Guid blogId, CreateBlogModel model)
    {
        return Ok(await _blogManager.UpdateBlog(blogId, model));
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteBlog(Guid blogId)
    {
        return Ok(await _blogManager.DeleteBlog(blogId));
    }
}