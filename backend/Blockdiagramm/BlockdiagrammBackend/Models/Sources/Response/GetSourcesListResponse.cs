using BlockdiagrammBackend.Models.Project;

namespace BlockdiagrammBackend.Models.Sources.Response
{
    public class GetSourcesListResponse
    {
        public List<SourceFile> Sources { get; set; }

        public GetSourcesListResponse(ProjectInstance projectInstance)
        {
            Sources = projectInstance.SourceFiles.ToList();
        }

        // Create a empty source list response (only for error)
        public GetSourcesListResponse()
        {
            Sources = new List<SourceFile>();
        }
    }
}
