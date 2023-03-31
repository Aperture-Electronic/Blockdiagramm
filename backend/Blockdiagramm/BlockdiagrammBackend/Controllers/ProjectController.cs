using BlockdiagrammBackend.Models.Project;
using BlockdiagrammBackend.Models.Project.Request;
using BlockdiagrammBackend.Models.Project.Response;
using BlockdiagrammBackend.Session;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BlockdiagrammBackend.Controllers
{
    [Route("[controller]/[action]")]
    public class ProjectController : Controller
    {
        // public ProjectInstance projectInstance;
        private readonly SessionStorage<ProjectInstance> projectInstance;

        public ProjectController(SessionStorage<ProjectInstance> projectInstance) => this.projectInstance = projectInstance;

        [HttpGet]
        public bool CheckProjectValid(SessionIndependRequest request)
        {
            if (!ModelState.IsValid)
            {
                if (ModelState.ContainsKey(nameof(request.SessionId)))
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                }

                return false;
            }

            return projectInstance[request].IsValid;
        }

        [HttpPost]
        public NewProjectResponse NewProject([FromBody] NewProjectRequest request)
        {
            if (!ModelState.IsValid)
            {
                if (ModelState.ContainsKey(nameof(request.SessionId)))
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                }

                return new NewProjectResponse(false, errorReason:
                    "Invalid request data (invalid session id, empty project name or path)");
            }

            try
            {
                projectInstance[request].NewProject(request.Name, request.Path);
            }
            catch (Exception ex)
            {
                return new NewProjectResponse(false, errorReason: ex.Message);
            }

            return new NewProjectResponse(true, projectName: request.Name);
        }

        [HttpPost]
        public void CloseProject() => CloseProject();
    }
}
