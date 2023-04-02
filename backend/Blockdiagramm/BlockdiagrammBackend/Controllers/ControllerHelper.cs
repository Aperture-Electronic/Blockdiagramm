using BlockdiagrammBackend.Session;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net;

namespace BlockdiagrammBackend.Controllers
{
    public static class ControllerHelper
    {
        public static bool CheckSessionIndependRequest(ModelStateDictionary modelState, HttpResponse response)
        {
            if (!modelState.IsValid)
            {
                if (modelState.ContainsKey(nameof(SessionIndependRequest.SessionId)))
                {
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                }

                return false;
            }

            return true;
        }
    }
}
