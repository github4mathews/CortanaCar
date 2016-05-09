using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using CortanaCarConsole;
using CortanaCarConsole.Services;
using Microsoft.Owin.Hosting;

public class Program
{
    public static MortorController MortorController { get; private set; }

    private static FileSystemWatcher fileWatcher { get; set; }

    static void Main(string[] args)
    {
        var breakEvent = new ManualResetEvent(initialState: false);
        Console.CancelKeyPress += (sende, e) => breakEvent.Set();

        using (Program.MortorController = new MortorController(AppSettings.Port))
        using (Program.fileWatcher = new FileSystemWatcher(AppSettings.WatchFolder, "*.cmd"))
        {
            fileWatcher.Created += FileWatcher_Created;
            fileWatcher.EnableRaisingEvents = true;

            var baseAddress = "http://localhost:8080/";
            using (var httpHost = WebApp.Start<Startup>(url: baseAddress))
            {
                Console.WriteLine($"Start {baseAddress}");
                Console.WriteLine("Ctrl+C to stop this program.");
                breakEvent.WaitOne();
            }
        }
    }

    private static void FileWatcher_Created(object sender, FileSystemEventArgs e)
    {
        File.Delete(e.FullPath);
        var match = Regex.Match(e.Name, @"^cortanacar-[0-9A-Z]{16}-(?<cmd>[^\.]+).cmd$");
        if (match.Success)
        {
            var command = match.Groups["cmd"].Value;
            Program.MortorController.Action(command, int.Parse(AppSettings.AutoStop));
            Console.WriteLine(command);
        }
    }
}
