using BlockdiagrammBackend.Session;

namespace BlockdiagrammBackend.Models.Elaborator.Response
{
    public class GetComponentResponse : IErrorableResponse
    {
        public Component? Component { get; }

        private bool IsComponentValid => Component != null;

        private readonly bool requestValid;

        public bool Success => IsComponentValid && requestValid;

        private readonly string errorReason = "";

        public string ErrorReason => false switch
        {
            _ when requestValid && IsComponentValid => "",
            _ when requestValid => "Can not found the component in the library",
            _ => errorReason
        };

        /// <summary>
        /// Create a response contains a component normally
        /// </summary>
        public GetComponentResponse(Component component)
        {
            Component = component;
            requestValid = true;
        }

        /// <summary>
        /// Create a response without a component, means the component is not found
        /// </summary>
        public GetComponentResponse()
        {
            Component = null;
            requestValid = true;
        }

        /// <summary>
        /// Create a response with a error reason, means the request is invalid
        /// </summary>
        public GetComponentResponse(string reason = "")
        {
            requestValid = false;
            Component = null;
            errorReason = reason;
        }
    }
}
