

using RabbitMQ.Client;
using RabbitMQ.Client.Events;

using var connection = new ConnectionFactory(){HostName = "localhost"}.CreateConnection();
using var channel = connection.CreateModel();
var queueName="LoadBalance";
channel.QueueDeclare(queueName,true,false,false);

var consumer = new EventingBasicConsumer(channel);

consumer.Received += Consumer_Received;

channel.BasicConsume(queueName, false, consumer);
void Consumer_Received(object? sender, BasicDeliverEventArgs e)
{

    var message = System.Text.Encoding.UTF8.GetString(e.Body.ToArray());
    System.Threading.Thread.Sleep(2000);
    Console.WriteLine($"[+] new message :" + message);
    channel.BasicAck(e.DeliveryTag)=false;
    
}

Console.WriteLine("Press button to exit program");
Console.ReadLine();
