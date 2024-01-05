namespace Shared.Core.Application.Services;

public interface IFileSystemService
{
    void DeleteFile(string filePath);

    void DeleteFiles(IEnumerable<string> filePaths);
}