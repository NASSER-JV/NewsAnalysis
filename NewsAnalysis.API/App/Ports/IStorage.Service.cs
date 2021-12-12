namespace NewsAnalysis.API.App.Ports;

public interface IStorageService
{
    public Task<Stream> GetFileStream(string key);

    public Task UploadFile(string filePath, string contentType, string key);
}