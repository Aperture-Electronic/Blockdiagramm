using BlockdiagrammBackend.Models.Globals;

namespace BlockdiagrammBackend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Create global application cancellation source
            Globals.AppCancellationTokenSource = new CancellationTokenSource();

            // Build and start the host
            IHost host = CreateHostBuilder(args).Build();

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
            Globals.AppCancellationTokenSource?.Cancel();
        }
    }
}