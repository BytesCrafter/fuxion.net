
using Fuxion.Service;
using System.Threading;

public class Program
{
    public static string[] arguments = null;

    public static void Main(string[] args)
    {
        Program.arguments = args;

        CreateHostBuilderMain(Program.arguments).Build().Run();

        /* Thread worker = new Thread(StartWorker);
         worker.IsBackground = true;
         worker.Start();*/
    }

    public static void StartHost()
    {
        var builder = WebApplication.CreateBuilder(Program.arguments);

        // Add services to the container.
        builder.Services.AddRazorPages();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthorization();
        app.MapRazorPages();
        app.Run();
    }

    static void StartWorker()
    {
        CreateHostBuilderMain(Program.arguments).Build().Run();
    }

    public static IHostBuilder CreateHostBuilderMain(string[] args) =>
       Host.CreateDefaultBuilder(args)
       .UseSystemd()
       .ConfigureServices(services =>
       {
           services.AddHostedService<Worker>();
           services.AddHostedService<WebService>();
       });
}