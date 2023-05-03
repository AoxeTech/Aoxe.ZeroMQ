namespace Dish;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Please input the ports which want to connect :");
        var ports = Console.ReadLine()?.Split(" ").Select(int.Parse);
        Console.WriteLine("Please input the topics :");
        var topics = Console.ReadLine()?.Split(" ");

        Console.WriteLine($"Dish socket has bind ports [{string.Join(",", ports)}].");
        Console.WriteLine($"Subscriber started for Topic : {string.Join(",", topics)}");
        using (var netRt = new NetMQRuntime())
        {
            netRt.Run(new Disher().Handle(ports, topics));
        }
    }
}