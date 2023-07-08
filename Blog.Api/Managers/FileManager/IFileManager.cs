namespace Blog.Api.Managers.FileManager
{
    public interface IFileManager
    {
        Task<string> SaveFileToWwwrootAsync(IFormFile logoFile, string folderName);
        void DeleteFile(string filePath);
    }
}
