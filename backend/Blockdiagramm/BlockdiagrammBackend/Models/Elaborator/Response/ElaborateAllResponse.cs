using BlockdiagrammBackend.Session;

namespace BlockdiagrammBackend.Models.Elaborator.Response
{
    public class ElaborateAllResponse : IErrorableResponse
    {
        public bool Success { get; set; }
        public string ErrorReason { get; set; }

        public List<ElaborateResult> Results { get; set; } = new List<ElaborateResult>();

        public ElaborateAllResponse(bool success, IEnumerable<ElaborateResult> results, string errorReason = "") 
        { 
            Success = success;
            ErrorReason = errorReason;
            Results.AddRange(results);
        }

        public ElaborateAllResponse(bool success, string errorReason = "")
        {
            Success = success;
            ErrorReason = errorReason;
        }
    }
}
