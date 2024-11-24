using System;
using System.Runtime.InteropServices;
using System.Text;

class Program
{
    private const int SIGUSR1 = 10;
    private const int SIGUSR2 = 12;

    private delegate void SignalHandler(int signal);

    [DllImport("libc")]
    private static extern int signal(int sig, SignalHandler handler);

    static void Main(string[] args)
    {
      
        signal(SIGUSR1, HandleSignal);
        signal(SIGUSR2, HandleSignal);

        Console.WriteLine("Program is running. Send SIGUSR1 or SIGUSR2 to this process.");
        Console.WriteLine($"PID: {Environment.ProcessId}");

        while (true)
        {
            System.Threading.Thread.Sleep(1000);
        }
    }

    private static void HandleSignal(int signal)
    {
        if (signal == SIGUSR1)
        {
            Console.WriteLine("SIGUSR1 received.");
        }
        else if (signal == SIGUSR2)
        {
            Console.WriteLine("SIGUSR2 received. Writing random ASCII characters and terminating.");
            WriteRandomAsciiCharacters();
            Environment.Exit(0);
        }
    }

    private static void WriteRandomAsciiCharacters()
    {
        Random random = new Random();
        StringBuilder sb = new StringBuilder(100);

        for (int i = 0; i < 100; i++)
        {
            char randomChar = (char)random.Next(32, 127);
            sb.Append(randomChar);
        }

        Console.WriteLine(sb.ToString());
    }
}
