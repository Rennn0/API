using API;
using Serilog;

internal class Program
{
    private static void Main(string[] args) =>
        WebHostBuilder(args).Build().Run();

    private static IHostBuilder WebHostBuilder(string[] args) =>
        Host
        .CreateDefaultBuilder(args)
        .UseSerilog()
        .ConfigureWebHostDefaults(builder => builder.UseStartup<Startup>());
}