using BlockdiagrammBackend.Models.Elaborator.Response;
using BlockdiagrammBackend.Models.Project;
using BlockdiagrammBackend.Session;
using Microsoft.AspNetCore.Mvc;

namespace BlockdiagrammBackend.Controllers
{
    public class ElaboratorController : Controller
    {
        private readonly SessionStorage<ProjectInstance> projectInstance;

        public ElaboratorController(SessionStorage<ProjectInstance> projectInstance) => this.projectInstance = projectInstance;

        [HttpGet] 
        public ElaborateAllResponse ElaborateAll(SessionIndependRequest request)
        {
            if (!ControllerHelper.CheckSessionIndependRequest(ModelState, Response))
            {
                return new(false, "Invalid request data (empty session id)");
            }

            ProjectInstance project = projectInstance[request];

            try
            {
                project.ElaborateAll();
            }
            catch (Exception ex)
            {
                return new(false, ex.Message);
            }

            return new(true);
        }
    }
}
