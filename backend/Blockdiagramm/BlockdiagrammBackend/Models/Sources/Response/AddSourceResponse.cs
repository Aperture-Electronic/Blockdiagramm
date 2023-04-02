using BlockdiagrammBackend.Session;

namespace BlockdiagrammBackend.Models.Sources.Response
{
    public class AddSourceResponse : IErrorableResponse
    {
        public bool Success { get; set; }

        public string ErrorReason { get; set; }

        public SourceFile? File { get; set; }

        public AddSourceResponse(bool success, SourceFile? file = null, string errorReason = "")
        {
            Success = success;
            ErrorReason = errorReason;
            File = file;
        }
    }
}
