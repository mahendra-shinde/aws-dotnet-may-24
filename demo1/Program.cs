using Amazon;
using Amazon.S3;
using Amazon.S3.Model;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
IAmazonS3 s3Client = new AmazonS3Client(RegionEndpoint.USEast1);
Console.WriteLine("Connected to S3 endpoint at east-us-1 region");
string bucketName="gt5s6t52tr";
string keyName="volume-1.txt";
string filePath="d:\\data";

await DownloadObjectFromBucketAsync(s3Client, bucketName, keyName, filePath);


static async Task<bool> DownloadObjectFromBucketAsync(
IAmazonS3 client,
string bucketName,
string objectName,
string filePath)
{
    // Create a GetObject request
    var request = new GetObjectRequest
    {
        BucketName = bucketName,
        Key = objectName,
    };

    // Issue request and remember to dispose of the response
    using GetObjectResponse response = await client.GetObjectAsync(request);

    try
    {
        // Save object to local file
        await response.WriteResponseStreamToFileAsync($"{filePath}\\{objectName}", true, CancellationToken.None);
        return response.HttpStatusCode == System.Net.HttpStatusCode.OK;
    }
    catch (AmazonS3Exception ex)
    {
        Console.WriteLine($"Error saving {objectName}: {ex.Message}");
        return false;
    }
}