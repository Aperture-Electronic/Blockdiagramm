using BlockdiagrammBackend.Models.Project;
using BlockdiagrammBackend.Models.Sources;
using BlockdiagrammBackend.Models.Sources.Request;
using BlockdiagrammBackend.Models.Sources.Response;
using BlockdiagrammBackend.Session;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BlockdiagrammBackend.Controllers
{
    [Route("[controller]/[action]")]
    public class SourceController : Controller
    {
        private readonly SessionStorage<ProjectInstance> projectInstance;

        public SourceController(SessionStorage<ProjectInstance> projectInstance)
        {
            this.projectInstance = projectInstance;
        }

        [HttpGet]
        public GetSourcesListResponse GetSourcesList(SessionIndependRequest request)
            => ControllerHelper.CheckSessionIndependRequest(ModelState, Response) ?
            new(projectInstance[request]) :
            new();

        [HttpPost]
        public AddSourceResponse AddSource([FromBody] AddSourceRequest request)
        {
            if (!ControllerHelper.CheckSessionIndependRequest(ModelState, Response))
            {
                return new AddSourceResponse(false, errorReason:
                    "Invalid request data (empty session id or source path)");
            }

            ProjectInstance project = projectInstance[request];

            // Make a new source file instance
            SourceFile sourceFile = (request.Type != SourceFileType.Auto) ?
                new(request.Path, request.Type) :
                new(request.Path);

            try
            {
                // Try to add to the project
                project.AddSource(sourceFile);
            }
            catch (Exception ex)
            {
                return new AddSourceResponse(false, errorReason: ex.Message);
            }

            return new AddSourceResponse(true, sourceFile);
        }
    }
}
