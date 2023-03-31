using BlockdiagrammBackend.Models.Globals;

namespace BlockdiagrammBackend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Build and start the host
            IHost host = CreateHostBuilder(args).Build();
            
            // Run the host with a global application cancellation token
            host.RunAsync(Globals.AppCancellationTokenSource.Token).Wait();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
            => Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });

        public static void Shutdown()
        {
            // When we need shutdown the server, use the cancellation token
            Globals.AppCancellationTokenSource?.Cancel();
        }
    }
}