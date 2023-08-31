
using Fuxion.Service;
using Fuxion.Controllers;
using Fuxion.Helpers;
using Microsoft.Extensions.Hosting;

public class Program
{
    public static string[] arguments = null;

    public static void Main(string[] args)
    {
        Program.arguments = args;

        CreateHostBuilderMain(Program.arguments).Build().Run();
    }

    public static void StartHost()
    {
        // Prepare the arguments for running the websocket server.
        string[] commands = StringUtil.Shift(Program.arguments, 1);
        string webip = Program.arguments.Length > 2 ? commands[0] : "127.0.0.1";
        int webport = commands.Length > 1 ? Int32.Parse(commands[1]) : 8000;

        FuxionServer server = new FuxionServer();
        server.Start(webip, webport);
    }

    public static IHostBuilder CreateHostBuilderMain(string[] args) =>
       Host.CreateDefaultBuilder(args)
       .UseSystemd()
       .ConfigureServices(services =>
       {
           services.AddHostedService<Worker>();
       });
}