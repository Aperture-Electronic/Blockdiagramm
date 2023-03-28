namespace BlockdiagrammBackend.Models.Server
{
    public class ServerMonitor
    {
        private readonly Thread serverMonitorThread;
        private readonly CancellationTokenSource cancellationTokenSource;
        public ServerStatus ServerStatus { get; }

        public int ServerTickIntervalMilliseconds => 1000;

        public ServerMonitor(CancellationTokenSource globalCancellationTokenSource)
        {
            ServerStatus = new ServerStatus();

            cancellationTokenSource = globalCancellationTokenSource;
            serverMonitorThread = new Thread(new ParameterizedThreadStart(ServerMonitorThread));
            serverMonitorThread.Start(cancellationTokenSource.Token);
        }

        private void ServerMonitorThread(object? arg)
        {
            if (arg is not CancellationToken cancellationToken)
            {
                throw new ArgumentException($"{nameof(arg)} must be an {nameof(CancellationToken)}");
            }

            for (; ; )
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    break;
                }

                ServerStatus.IncreaseServerTick();
                Thread.Sleep(ServerTickIntervalMilliseconds);
            }
        }
    }
}
