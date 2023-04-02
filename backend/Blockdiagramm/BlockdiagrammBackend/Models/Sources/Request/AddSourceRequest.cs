using BlockdiagrammBackend.Session;
using System.ComponentModel.DataAnnotations;

namespace BlockdiagrammBackend.Models.Sources.Request
{
    public class AddSourceRequest : SessionIndependRequest
    {
        [Required]
        public string Path { get; set; } = null!;

        public SourceFileType Type { get; set; } = SourceFileType.Auto;
    }
}
