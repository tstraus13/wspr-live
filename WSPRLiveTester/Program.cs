using WSPRLive;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

var client = new Client();

var test = await client.GetSpots(DateTime.Now.Subtract(TimeSpan.FromHours(8)), DateTime.Now, 3570039, 14097020, 7007, 12921);

Console.WriteLine("END");