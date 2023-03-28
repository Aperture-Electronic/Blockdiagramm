namespace BlockdiagrammBackend.Models.Server
{
    [Serializable]
    public class ServerStatus
    {
        public ServerStatusCode StatusCode { get; }
        public DateTime StartTime { get; }
        public double RunningTime => (DateTime.Now - StartTime).TotalMilliseconds;

        private int serverTick;

        public int ServerTick => serverTick;

        public ServerStatus()
        {
            serverTick = 0;
            StatusCode = ServerStatusCode.Idle;
            StartTime = DateTime.Now;
        }

        public void IncreaseServerTick() => Interlocked.Increment(ref serverTick);
    }
}
