namespace Application.Common.Interfaces;

public interface IS3Service
{
    Task<string> UploadFile(Stream fileStream, string fileName);
    Task DeleteFile(string fileName);
}
