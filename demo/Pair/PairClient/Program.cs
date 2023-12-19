namespace PairClient;

class Program
{
    static void Main(string[] args)
    {
        using (var pairSocket = new PairSocket())
        {
            Console.WriteLine("Socket start connect...");
            pairSocket.Connect("tcp://localhost:6000");
            Console.WriteLine("Socket connect success.");
            while (true)
            {
                var msg = pairSocket.ReceiveFrameBytes();
                Console.WriteLine(Encoding.UTF8.GetString(msg));
                pairSocket.SendFrame(
                    Encoding.UTF8.GetBytes($"client message to server1 in [{DateTime.Now}]")
                );
                pairSocket.SendFrame(
                    Encoding.UTF8.GetBytes($"client message to server2 in [{DateTime.Now}]")
                );
                Thread.Sleep(1000);
            }
        }
    }
}
