using BlockdiagrammBackend.Session;

namespace BlockdiagrammBackend.Models.Elaborator
{
    public class ElaborateResult : IErrorableResponse
    {
        public string FileHash { get; }
        public bool Success { get; set; }
        public string ErrorReason { get; set; }

        public List<string> Components { get; set; } = new List<string>();

        public ElaborateResult(bool success, string fileHash, string errorReason = "")
        {
            Success = success;
            ErrorReason = errorReason;
            FileHash = fileHash;
        }

        public ElaborateResult(bool success, string fileHash, IEnumerable<string> componentName, string errorReason = "")
        {
            Success = success;
            ErrorReason = errorReason;
            FileHash = fileHash;
            Components.AddRange(componentName);
        }
    }
}
