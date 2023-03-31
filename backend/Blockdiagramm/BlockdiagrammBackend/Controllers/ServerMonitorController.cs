using BlockdiagrammBackend.Models.Server;
using Microsoft.AspNetCore.Mvc;

namespace BlockdiagrammBackend.Controllers
{
    [Route("[controller]/[action]")]
    public class ServerMonitorController : Controller
    {
        private readonly ServerMonitor serverMonitor;

        public ServerMonitorController(ServerMonitor serverMonitor) => this.serverMonitor = serverMonitor;

        [HttpGet]
        public ServerStatus CheckServerStatus() => serverMonitor.ServerStatus;

        [HttpPost]
        public void StopServer() => Program.Shutdown();
    }
}
