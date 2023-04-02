using BlockdiagrammBackend.Models.Project;
using BlockdiagrammBackend.Models.Project.Request;
using BlockdiagrammBackend.Models.Project.Response;
using BlockdiagrammBackend.Session;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net;

namespace BlockdiagrammBackend.Controllers
{
    [Route("[controller]/[action]")]
    public class ProjectController : Controller
    {
        private readonly SessionStorage<ProjectInstance> projectInstance;

        public ProjectController(SessionStorage<ProjectInstance> projectInstance) => this.projectInstance = projectInstance;

        

        [HttpGet]
        public bool CheckProjectValid(SessionIndependRequest request) 
            => ControllerHelper.CheckSessionIndependRequest(ModelState, Response) && projectInstance[request].IsValid;

        [HttpGet]
        public string GetProjectName(SessionIndependRequest request)
            => ControllerHelper.CheckSessionIndependRequest(ModelState, Response) ? projectInstance[request].Name : "";

        [HttpPost]
        public NewProjectResponse NewProject([FromBody] NewProjectRequest request)
        {
            if (!ControllerHelper.CheckSessionIndependRequest(ModelState, Response))
            {
                return new NewProjectResponse(false, errorReason:
                    "Invalid request data (invalid session id, empty project name or path)");
            }

            ProjectInstance project = projectInstance[request];

            try
            {
                project.NewProject(request.Name, request.Path);
            }
            catch (Exception ex)
            {
                return new NewProjectResponse(false, errorReason: ex.Message);
            }

            return new NewProjectResponse(true, project);
        }

        [HttpPost]
        public void CloseProject() => CloseProject();
    }
}
