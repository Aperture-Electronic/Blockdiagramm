using BlockdiagrammBackend.Models.Project;
using BlockdiagrammBackend.Models.Server;

namespace BlockdiagrammBackend.Models.Globals
{
    public static class Globals
    {
        public static CancellationTokenSource AppCancellationTokenSource { get; } = new();
    }
}
