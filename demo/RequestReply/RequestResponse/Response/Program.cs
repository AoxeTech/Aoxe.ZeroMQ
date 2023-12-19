namespace Response;

class Program
{
    static void Main(string[] args)
    {
        using (var responseSocket = new ResponseSocket("@tcp://*:5001"))
        {
            Console.WriteLine($"[{DateTime.Now}]Server start success.");
            while (true)
            {
                var msg = responseSocket.ReceiveFrameString();
                Console.WriteLine($"[{DateTime.Now}]Receive \"{msg}\"");
                responseSocket.SendFrame($"Pass back——{msg} to you too");
            }
        }
    }
}
