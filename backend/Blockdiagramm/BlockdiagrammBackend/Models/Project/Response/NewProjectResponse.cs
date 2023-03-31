namespace BlockdiagrammBackend.Models.Project.Response
{
    [Serializable]
    public class NewProjectResponse
    {
        public bool Success { get; set; }

        public string ErrorReason { get; set; }

        public string ProjectName { get; set; }

        public NewProjectResponse(bool success, string projectName = "", string errorReason = "")
        {
            Success = success;
            ErrorReason = errorReason;
            ProjectName = projectName;
        }
    }
}
