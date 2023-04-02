using BlockdiagrammBackend.Session;

namespace BlockdiagrammBackend.Models.Elaborator.Response
{
    public class ElaborateAllResponse : IErrorableResponse
    {
        public bool Success { get; set; }
        public string ErrorReason { get; set; }

        public ElaborateAllResponse(bool success, string errorReason = "") 
        { 
            Success = success;
            ErrorReason = errorReason;
        }
    }
}
