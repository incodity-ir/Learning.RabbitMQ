
using RabbitMQ.Client;

using var connection = new ConnectionFactory(){HostName = "localhost"}.CreateConnection();
using var channel = connection.CreateModel();
var queueName = "LoadBalance";
channel.QueueDeclare(queueName,true,false,false);
Console.WriteLine("Please write your message and Press [ENTER] : \n");
var msg = Console.ReadLine();
var msgprop = channel.CreateBasicProperties();
msgprop.Persistent = true;
for (int i = 0; i < 20; i++)
{
    var message =$"{i}" + msg ;
    var messageBinary = System.Text.Encoding.UTF8.GetBytes(message).ToArray();
    channel.BasicPublish("", queueName, null, messageBinary);
}

Console.WriteLine("your message publish successed");
Console.WriteLine("Press button to exit program");
Console.ReadLine();
