// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
var snsClient = new AmazonSimpleNotificationServiceClient(RegionEndpoint.USEast1);

// SNS Topic ARN
var topicArn = "arn:aws:sns:us-east-1:992382694463:today-news";

// Message and Subject
var message = "The quick brown fox jumps over the lazy dog.";
var subject = "National News";