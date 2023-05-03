namespace Pusher;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Please input send quantity or exit : ");
        var input = Console.ReadLine();
        Console.WriteLine($"Input : {input}");
        using (var pusher = new PushSocket())
        {
            pusher.Bind("tcp://*:6009");
            Console.WriteLine("Pusher bind success.");
            while (!string.IsNullOrWhiteSpace(input) && input is not "exit")
            {
                var quantity = int.Parse(input);
                for (var i = 0; i < quantity; i++)
                    pusher.SendFrame($"Pusher sent {i}");
                Console.WriteLine("Please input send quantity or exit : ");
                input = Console.ReadLine();
            }
        }
    }
}