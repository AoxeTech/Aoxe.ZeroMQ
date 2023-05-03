namespace Worker;

class Program
{
    static void Main(string[] args)
    {
        // Task Worker
        // Connects PULL socket to tcp://localhost:5557
        // collects workload for socket from Ventilator via that socket
        // Connects PUSH socket to tcp://localhost:5558
        // Sends results to Sink via that socket
        Console.WriteLine("====== WORKER ======");
        using (var receiver = new PullSocket(">tcp://localhost:5557"))
        using (var sender = new PushSocket(">tcp://localhost:5558"))
        {
            //process tasks forever
            while (true)
            {
                //workload from the vetilator is a simple delay
                //to simulate some work being done, see
                //Ventilator.csproj Proram.cs for the workload sent
                //In real life some more meaningful work would be done
                var workload = receiver.ReceiveFrameString();
                //simulate some work being done
                Thread.Sleep(int.Parse(workload));
                //send results to sink, sink just needs to know worker
                //is done, message content is not important, just the presence of
                //a message means worker is done.
                //See Sink.csproj Program.cs
                Console.WriteLine("Sending to Sink");
                sender.SendFrame(string.Empty);
            }
        }
    }
}