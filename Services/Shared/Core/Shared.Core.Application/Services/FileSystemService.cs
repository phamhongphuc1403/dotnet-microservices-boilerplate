namespace Shared.Core.Application.Services;

public class FileSystemService : IFileSystemService
{
    public void DeleteFile(string filePath)
    {
        if (File.Exists(filePath)) File.Delete(filePath);
    }

    public void DeleteFiles(IEnumerable<string> filePaths)
    {
        Parallel.ForEach(filePaths, DeleteFile);
    }
}