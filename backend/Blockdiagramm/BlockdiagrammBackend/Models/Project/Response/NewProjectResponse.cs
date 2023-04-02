using BlockdiagrammBackend.Session;

namespace BlockdiagrammBackend.Models.Project.Response
{
    [Serializable]
    public class NewProjectResponse : IErrorableResponse
    {
        public bool Success { get; set; }

        public string ErrorReason { get; set; }

        public ProjectInstance? Project { get; set; }

        public NewProjectResponse(bool success, ProjectInstance? project = null, string errorReason = "")
        {
            Success = success;
            ErrorReason = errorReason;
            Project = project;
        }
    }
}
