using Blog.Api.Entities;
using Blog.Api.Managers.BlogManager;
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
        var blog = await _blogManager.GetBlogById(blogId);
        if (blog == null)
        {
            throw new Exception("Blog not found");
        }
        return Ok(blog);
    }

    [HttpGet("userId")]
    public async Task<IActionResult> GetBlogByAuthor(Guid userId)
    {
        var user = await _blogManager.GetBlogByAuthor(userId);
        if (user == null) { throw new Exception("User not found"); }
        return Ok(user);
    }

    [HttpGet("userName")]
    public async Task<IActionResult> GetBlogByAuthor(string userName)
    {
        var user = await _blogManager.GetBlogByAuthor(userName);
        if (user == null) { throw new Exception("User not found"); }
        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> CreateBlog([FromForm] CreateBlogModel model)
    {
        try
        {
            var result = await _blogManager.CreateBlog(model);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while creating the blog.");
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateBlog(Guid blogId, CreateBlogModel model)
    {

        var result = await _blogManager.UpdateBlog(blogId, model);
        return Ok(result);

    }

    [HttpDelete]
    public async Task<IActionResult> DeleteBlog(Guid blogId)
    {
        try
        {
            var result = await _blogManager.DeleteBlog(blogId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while deleting the blog.");
        }
    }
}