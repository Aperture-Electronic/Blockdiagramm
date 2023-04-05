using BlockdiagrammBackend.Models.Elaborator;
using BlockdiagrammBackend.Models.Elaborator.Request;
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
            List<ElaborateResult> result;

            try
            {
                result = project.ElaborateAll();
            }
            catch (Exception ex)
            {
                return new(false, ex.Message);
            }

            return new(true, result);
        }

        [HttpGet]
        public GetComponentsListResponse GetComponentsList(SessionIndependRequest request)
            => ControllerHelper.CheckSessionIndependRequest(ModelState, Response) ?
               new(projectInstance[request]) :
               new();

        [HttpPost]
        public GetComponentResponse GetComponent([FromBody] GetComponentRequest request)
        {
            // Check if the session independ request valid
            if (!ControllerHelper.CheckSessionIndependRequest(ModelState, Response))
            {
                return new("Invalid request data (empty session id or hash)");
            }

            // Get the project instance from the session storage
            ProjectInstance project = projectInstance[request];

            // Try to query the component
            var query = from component in project.ComponentsList
                        where component.Hash == request.Hash
                        select component;

            if (query.Any())
            {
                // Found the component
                return new(query.First());
            }

            // Not found the component
            return new();
        }
    }
}
