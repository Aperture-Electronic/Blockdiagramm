using System.ComponentModel.DataAnnotations;

namespace BlockdiagrammBackend.Session
{
    public class SessionIndependRequest
    {
        [Required]
        public string SessionId { get; set; } = null!;
    }
}
