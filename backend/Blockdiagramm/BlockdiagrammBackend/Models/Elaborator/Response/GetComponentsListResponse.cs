using BlockdiagrammBackend.Models.Project;

namespace BlockdiagrammBackend.Models.Elaborator.Response
{
    public class GetComponentsListResponse
    {
        public Dictionary<string, IEnumerable<SimplifiedComponent>> Components { get; set; }

        public GetComponentsListResponse(ProjectInstance projectInstance)
        {
            var components = projectInstance.ComponentsList.ToList();
            var groupBySource = from component in components
                                group component by component.File.Hash
                                into fileGroup select fileGroup;

            Components = new Dictionary<string, IEnumerable<SimplifiedComponent>>();
            foreach (var file in groupBySource)
            {
                Components.Add(file.Key, file.Select((e) => new SimplifiedComponent(e)));
            }
        }

        public GetComponentsListResponse()
        {
            Components = new();
        }
    }
}
