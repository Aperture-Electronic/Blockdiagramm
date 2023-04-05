namespace BlockdiagrammBackend.Models.Elaborator
{
    public class SimplifiedComponent
    {
        private readonly Component component;

        public string Hash => component.Hash;

        public string Name => component.Name;

        public SimplifiedComponent(Component component) => this.component = component;
    }
}
