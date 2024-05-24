// See https://aka.ms/new-console-template for more information
using Amazon.SQS;
using Amazon.SQS.Model;

Console.WriteLine("Hello, World!");
IAmazonSQS sqs = new AmazonSQSClient();
Console.WriteLine("SQS client created");
string queueUrl = "https://sqs.us-east-1.amazonaws.com/992382694463/queue1";
await SendMessage(sqs, queueUrl, "Hello, India!");
Console.WriteLine("Message sent");

static async Task SendMessage(
      IAmazonSQS sqsClient, string qUrl, string messageBody)
    {
      SendMessageResponse responseSendMsg =
        await sqsClient.SendMessageAsync(qUrl, messageBody);
      Console.WriteLine($"Message added to queue\n  {qUrl}");
      Console.WriteLine($"HttpStatusCode: {responseSendMsg.HttpStatusCode}");
    }