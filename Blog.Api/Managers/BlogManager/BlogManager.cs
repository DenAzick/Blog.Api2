using Blog.Api.Context;
using Blog.Api.Entities;
using Blog.Api.Models.BlogModels;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Blog.Api.Managers.BlogManager;

public class BlogManager
{
    private readonly AppDbContext _dbContext;

    public BlogManager(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<BlogModel>> GetBlogs()
    {
        try
        {
            var blogs = await _dbContext.Blogs.Include(b => b.Posts).ToListAsync();
            return ParseList(blogs);
        }
        catch (Exception ex)
        {
            return new List<BlogModel>(); 
        }
    }

    public async Task<BlogModel> GetBlogById(Guid id)
    {
        var blog = _dbContext.Blogs.FirstOrDefault(b => b.Id == id);
        if (blog == null) throw new Exception("Not found");
        return ParseToBlogModel(blog);
    }

    public async Task<List<BlogModel>> GetBlogByAuthor(Guid userId)
    {
        var blogs = await _dbContext.Blogs.Where(b => b.UserId == userId).ToListAsync();
        if (blogs == null) throw new Exception("Not found");

        return ParseList(blogs);
    }
    public async Task<List<BlogModel>> GetBlogByAuthor(string username)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == username);
        if (user == null) throw new Exception("Not found such kinda user");
        var blogs = user.Blogs;
        if (blogs == null) throw new Exception("This user has no blogs");

        return ParseList(blogs);
    }
    public async Task<BlogModel> CreateBlog(CreateBlogModel model)
    {
        var blog = new Entities.Blog()
        {
            Name = model.Name,
            Description = model.Description,
            UserId = model.UserId
        };
        _dbContext.Blogs.Add(blog);
        await _dbContext.SaveChangesAsync();
        return ParseToBlogModel(blog);
    }
    public async Task<BlogModel> UpdateBlog(Guid blogId, CreateBlogModel model)
    {
        var blog = _dbContext.Blogs.FirstOrDefault(b => b.Id == blogId);
        if (blog == null) throw new Exception("Not found");

        blog.Name = model.Name;
        blog.Description = model.Description;
        await _dbContext.SaveChangesAsync();
        return ParseToBlogModel(blog);
    }

    public async Task<string> DeleteBlog(Guid blogId)
    {
        var blog = await _dbContext.Blogs.FindAsync(blogId);

        if (blog == null)
        {
            throw new Exception("Blog not found."); 
        }

        _dbContext.Blogs.Remove(blog);
        await _dbContext.SaveChangesAsync();
        return "No content";
    }

    private BlogModel ParseToBlogModel(Entities.Blog blog)
    {
        var blogModel = new BlogModel()
        {
            Id = blog.Id,
            Name = blog.Name,
            Description = blog.Description,
            CreatedDate = (DateTime)blog.CreatedDate,
            UserId = blog.UserId,
            UserName = GetUserName(blog.UserId),
            Posts = blog.Posts,
        };
        return blogModel;
    }

    private string GetUserName(Guid userId)
    {
        var user = _dbContext.Users.Find(userId);

        if (user == null)
        {
            throw new Exception("User not found."); 
        }
        return user.Username;
    }

    private List<BlogModel> ParseList(List<Blog.Api.Entities.Blog> blogs)
    {
        var blogModels = new List<BlogModel>();
        foreach (var blog in blogs)
        {
            blogModels.Add(ParseToBlogModel(blog));
        }
        return blogModels;
    }

    
}