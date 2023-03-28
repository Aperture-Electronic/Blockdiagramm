using BlockdiagrammBackend.Models.Globals;
using BlockdiagrammBackend.Models.Server;
using Microsoft.AspNetCore.Mvc;

namespace BlockdiagrammBackend.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ServerMonitorController : Controller
    {
        public ServerMonitorController()
        {

        }

        [HttpGet]
        public ServerStatus? CheckServerStatus()
        {
            return Globals.ServerMonitor?.ServerStatus;
        }

        [HttpPost]
        public void StopServer()
        {
            Program.Shutdown();
        }
    }
}
