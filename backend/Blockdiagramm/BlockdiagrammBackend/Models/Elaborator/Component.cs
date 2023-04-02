using BlockdiagrammBackend.Models.Sources;
using HDLElaborateRoslyn.Elaborated;

namespace BlockdiagrammBackend.Models.Elaborator
{
    public class Component
    {
        public ElaboratedModule Module { get; }

        public SourceFile File { get; }

        public Component(SourceFile file, ElaboratedModule module)
        {
            File = file;
            Module = module;
        }
    }
}
