using BlockdiagrammBackend.Session;
using System.ComponentModel.DataAnnotations;

namespace BlockdiagrammBackend.Models.Project.Request
{
    [Serializable]
    public class NewProjectRequest : SessionIndependRequest
    {
        [Required, StringLength(int.MaxValue, MinimumLength = 1)]
        public string Name { get; set; } = null!;

        [Required, StringLength(int.MaxValue, MinimumLength = 1)]
        public string Path { get; set; } = null!;
    }
}
