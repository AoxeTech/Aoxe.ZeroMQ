namespace ScatterGather;

class Program
{
    static async Task Main(string[] args)
    {
        using var scatter1 = new ScatterSocket();
        using var scatter2 = new ScatterSocket();
        using var gather1 = new GatherSocket();
        using var gather2 = new GatherSocket();
        using var gather3 = new GatherSocket();

        scatter1.Bind("inproc://test-scatter-gather1");
        Console.WriteLine("Scatter1 bind test-scatter-gather1.");
        scatter2.Bind("inproc://test-scatter-gather2");
        Console.WriteLine("Scatter2 bind test-scatter-gather2.");

        scatter1.Bind("inproc://test-scatter-gather3");
        Console.WriteLine("Scatter1 bind test-scatter-gather3.");
        scatter2.Bind("inproc://test-scatter-gather4");
        Console.WriteLine("Scatter2 bind test-scatter-gather4.");
            
        gather1.Connect("inproc://test-scatter-gather1");
        Console.WriteLine("Gather1 connected gather1.");
        gather1.Connect("inproc://test-scatter-gather2");
        Console.WriteLine("Gather1 connected gather2.");
            
        gather2.Connect("inproc://test-scatter-gather2");
        Console.WriteLine("Gather2 connected gather2.");
        gather2.Connect("inproc://test-scatter-gather3");
        Console.WriteLine("Gather2 connected gather3.");
            
        gather3.Connect("inproc://test-scatter-gather3");
        Console.WriteLine("Gather3 connected gather3.");
        gather3.Connect("inproc://test-scatter-gather2");
        Console.WriteLine("Gather3 connected gather2.");

        await scatter1.SendAsync("1");
        Console.WriteLine("Scatter1 sent 1");
        await scatter2.SendAsync("2");
        Console.WriteLine("Scatter2 sent 2");
        await scatter1.SendAsync("3");
        Console.WriteLine("Scatter1 sent 3");
        await scatter2.SendAsync("4");
        Console.WriteLine("Scatter2 sent 4");
        await scatter1.SendAsync("5");
        Console.WriteLine("Scatter1 sent 5");
        await scatter2.SendAsync("6");
        Console.WriteLine("Scatter2 sent 6");

        var m1 = await gather1.ReceiveStringAsync();
        Console.WriteLine($"Gather1 received {m1}");
        var m2 = await gather1.ReceiveStringAsync();
        Console.WriteLine($"Gather1 received {m2}");
        var m3 = await gather2.ReceiveStringAsync();
        Console.WriteLine($"Gather2 received {m3}");
        var m4 = await gather2.ReceiveStringAsync();
        Console.WriteLine($"Gather2 received {m4}");
        var m5 = await gather3.ReceiveStringAsync();
        Console.WriteLine($"Gather3 received {m5}");
        var m6 = await gather3.ReceiveStringAsync();
        Console.WriteLine($"Gather3 received {m6}");

        Console.ReadLine();
    }
}