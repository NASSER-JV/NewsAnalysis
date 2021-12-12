using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Options;
using NewsAnalysis.API.App.Ports;
using NewsAnalysis.API.Infrastructure.Configurations;

namespace NewsAnalysis.API.Infrastructure.Adapters;

public class StorageService : IStorageService
{
    public StorageService(IOptions<AwsS3Config> configuration)
    {
        Client = new AmazonS3Client(
            new BasicAWSCredentials(configuration.Value.AccessKey, configuration.Value.SecretKey),
            RegionEndpoint.USEast2);

        ValidateBucket();
    }

    private AmazonS3Client Client { get; }
    private static string BucketName => "news-analysis-tcc-unirp";

    public async Task<Stream> GetFileStream(string key)
    {
        var file = await Client.GetObjectAsync(new GetObjectRequest { BucketName = BucketName, Key = key });

        return file.ResponseStream;
    }

    public async Task UploadFile(string filePath, string contentType, string key)
    {
        await Client.PutObjectAsync(new PutObjectRequest
        {
            BucketName = BucketName,
            Key = key,
            ContentType = contentType,
            FilePath = filePath
        });
    }

    private void ValidateBucket()
    {
        var buckets = Client.ListBucketsAsync().Result;

        if (buckets.Buckets.All(bucket => bucket.BucketName != BucketName))
            Client.PutBucketAsync(new PutBucketRequest
            {
                BucketName = BucketName,
                BucketRegion = S3Region.USEast2,
                CannedACL = S3CannedACL.Private
            }).Wait();
    }
}