using System;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Amazon.Lambda.S3Events;
using Amazon.S3;
using Amazon.S3.Util;
using Amazon.SQS;
using Amazon.SQS.Model;
using System.Text;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer ))]
namespace fun102;
public class Function
{
  private static readonly string SQS_QUEUE_URL = Environment.GetEnvironmentVariable("SQS_QUEUE_URL");

        private readonly IAmazonS3 _s3Client;
        private readonly IAmazonSQS _sqsClient;

        public Function() : this(new AmazonS3Client(), new AmazonSQSClient()) { }

        public Function(IAmazonS3 s3Client, IAmazonSQS sqsClient)
        {
            _s3Client = s3Client ?? throw new ArgumentNullException(nameof(s3Client));
            _sqsClient = sqsClient ?? throw new ArgumentNullException(nameof(sqsClient));
        }

        public async Task FunctionHandler(S3Event evnt, ILambdaContext context)
        {
            foreach (var record in evnt.Records)
            {
                var s3 = record.S3;
                context.Logger.LogLine($"Processing S3 event for bucket {s3.Bucket.Name} and key {s3.Object.Key}");

                var messageBody = $"New object uploaded: {s3.Bucket.Name}/{s3.Object.Key}";

                var sendMessageRequest = new SendMessageRequest
                {
                    QueueUrl = SQS_QUEUE_URL,
                    MessageBody = messageBody
                };

                await _sqsClient.SendMessageAsync(sendMessageRequest);
            }
        }
}
