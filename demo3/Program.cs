// See https://aka.ms/new-console-template for more information
using Amazon;
using Amazon.SQS;
using Amazon.SQS.Model;

Console.WriteLine("Hello, World!");

var sqsClient = new AmazonSQSClient(RegionEndpoint.USEast1);

var queueUrl = "https://sqs.us-east-1.amazonaws.com/992382694463/orders.fifo";

var receiveMessageRequest = new ReceiveMessageRequest
{
    QueueUrl = queueUrl,
    MaxNumberOfMessages = 1,
    WaitTimeSeconds = 10
};

var receiveMessageResponse = await sqsClient.ReceiveMessageAsync(receiveMessageRequest);
if (receiveMessageResponse.Messages.Count > 0)
{
    var message = receiveMessageResponse.Messages[0];
    Console.WriteLine($"Received message: {message.Body}");

    await sqsClient.DeleteMessageAsync(new DeleteMessageRequest
    {
        QueueUrl = queueUrl,
        ReceiptHandle = message.ReceiptHandle
    });
}

