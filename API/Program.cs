using API;

internal class Program
{
    private static void Main(string[] args) =>
        WebHostBuilder(args).Build().Run(); 

    private static IHostBuilder WebHostBuilder(string[] args) =>
        Host
        .CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(builder => builder.UseStartup<Startup>());
 
}