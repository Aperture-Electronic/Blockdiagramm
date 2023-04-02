using System.Collections.Concurrent;
using System.Diagnostics;

namespace BlockdiagrammBackend.Session
{
    public class SessionStorage<T> where T : class
    {
        public TimeSpan Timeout { get; }
        public int ExpiredScanTime { get; }

        private readonly ConcurrentDictionary<string, T> sessionDictionary;
        private readonly ConcurrentDictionary<string, Stopwatch> sessionRegisterDictionary;
        private readonly Thread sessionExpiredMonitorThread;
        private readonly object sessionLock = new();

        public SessionStorage(TimeSpan timeout, CancellationTokenSource cancellationTokenSource, int expiredScanTime = 1000)
        {
            Timeout = timeout;
            ExpiredScanTime = expiredScanTime;

            sessionDictionary = new ConcurrentDictionary<string, T>();
            sessionRegisterDictionary = new ConcurrentDictionary<string, Stopwatch>();
            sessionExpiredMonitorThread = new Thread(new ParameterizedThreadStart(SessionExpiredMonitorThread));
            sessionExpiredMonitorThread.Start(cancellationTokenSource.Token);  
        }

        public T this[string sessionId] => GetOrAdd(sessionId);

        public T this[SessionIndependRequest request] => this[request.SessionId];

        public T GetOrAdd(string sessionId)
        {
            sessionId = sessionId.ToLower();

            lock (sessionLock)
            {
                if (sessionDictionary.TryGetValue(sessionId, out T? value))
                {
#if DEBUG
                    Console.WriteLine($"Session stroage of {sessionId} has been accessed of {nameof(SessionStorage<T>)}<{typeof(T).Name}>");
#endif
                    // Reset the timeout
                    lock (sessionLock)
                    {
                        sessionRegisterDictionary[sessionId].Restart();

                        return value;
                    }
                }

                if (Activator.CreateInstance(typeof(T), new object[] { sessionId }) is not T newSessionInstance)
                {
                    throw new Exception("The object in the session storage can not be construct by a session id");
                }

                sessionDictionary.TryAdd(sessionId, newSessionInstance);
                Stopwatch timeoutWatch = new Stopwatch();
                timeoutWatch.Start();
                sessionRegisterDictionary.TryAdd(sessionId, timeoutWatch);
#if DEBUG
                Console.WriteLine($"New session stroage of {sessionId} has been added of {nameof(SessionStorage<T>)}<{typeof(T).Name}>");
#endif
                return sessionDictionary[sessionId];
            }
        }

        private void RemoveExpired(string sessionId)
        {
            lock (sessionLock)
            {
                sessionDictionary.TryRemove(sessionId, out _);
                sessionRegisterDictionary.TryRemove(sessionId, out _);
            }
        }

        private void SessionExpiredMonitorThread(object? arg)
        {
            if (arg is not CancellationToken cancellationToken)
            {
                throw new Exception("The argument of thread must be a cancellation token");
            }

            List<string> toRemove = new();

            for (; ; )
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    break;
                }

                toRemove.Clear();

                lock (sessionLock)
                {
                    foreach (var session in sessionRegisterDictionary)
                    {
                        Stopwatch timeoutWatch = session.Value;
                        if (timeoutWatch.Elapsed > Timeout)
                        {
                            toRemove.Add(session.Key);
                        }
                    }

                    foreach (var sessionId in toRemove)
                    {
                        RemoveExpired(sessionId);
#if DEBUG
                        Console.WriteLine($"Session {sessionId} has been cleared of {nameof(SessionStorage<T>)}<{typeof(T).Name}>");
#endif
                    }
                }

                Thread.Sleep(ExpiredScanTime);
            }
        }
    }
}
