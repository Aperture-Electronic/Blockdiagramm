using BlockdiagrammBackend.Session;
using System.ComponentModel.DataAnnotations;

namespace BlockdiagrammBackend.Models.Elaborator.Request
{
    public class GetComponentRequest : SessionIndependRequest
    {
        [Required]
        public string Hash { get; set; } = null!;
    }
}
