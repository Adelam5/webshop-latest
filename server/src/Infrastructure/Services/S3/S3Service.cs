using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Util;
using Application.Common.Interfaces;
using Domain.Exceptions.S3;
using Domain.S3.Exceptions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net;

namespace Infrastructure.Services.S3;
public class S3Service : IS3Service
{
    private readonly IAmazonS3 s3Client;
    private readonly string bucketName;
    private readonly ILogger<S3Service> logger;

    public S3Service(IOptions<S3Settings> options, ILogger<S3Service> logger)
    {
        var settings = options.Value;
        s3Client = new AmazonS3Client(settings.Key, settings.Secret, Amazon.RegionEndpoint.EUCentral1);
        bucketName = settings.BucketName;
        this.logger = logger;
    }

    public async Task<string> UploadFile(Stream fileStream, string fileName)
    {
        try
        {
            var bucketExists = await AmazonS3Util.DoesS3BucketExistV2Async(s3Client, bucketName);

            if (!bucketExists)
            {
                var bucketRequest = new PutBucketRequest()
                {
                    BucketName = bucketName
                };

                await s3Client.PutBucketAsync(bucketRequest);
            }

            var objectRequest = new PutObjectRequest()
            {
                BucketName = bucketName,
                Key = fileName,
                InputStream = fileStream
            };

            var response = await s3Client.PutObjectAsync(objectRequest);

            bool isSuccess = response.HttpStatusCode == HttpStatusCode.OK;

            return $"https://{bucketName}.s3.amazonaws.com/{fileName}"; // To do: decide what to return,
        }
        catch (AmazonS3Exception ex)
        {
            logger.LogError(ex, ex.Message);
            throw new S3FileUploadException(ex.Message);
        }
    }

    public async Task DeleteFile(string fileName)
    {
        try
        {
            var response = await s3Client.DeleteObjectAsync(bucketName, fileName);
        }
        catch (AmazonS3Exception ex)
        {
            logger.LogError(ex, ex.Message);
            throw new S3FileDeletionException(ex.Message);
        }
    }
}
