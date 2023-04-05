using HDLAbstractSyntaxTree.Types;
using HDLElaborateRoslyn.Elaborated;

namespace BlockdiagrammBackend.Models.Interface
{
    [Serializable]
    public class SimplifiedModulePort
    {
        private readonly ElaboratedModulePort port;

        public string Name => port.Name;
        public ModulePortDirection Direction => port.Direction switch
        {
            HDLAbstractSyntaxTree.Types.Direction.In => ModulePortDirection.Input,
            HDLAbstractSyntaxTree.Types.Direction.Out => ModulePortDirection.Output,
            HDLAbstractSyntaxTree.Types.Direction.Inout => ModulePortDirection.Inout,
            _ => throw new Exception("The port must be a module port with direction in, out or inout")
        };

        public ElaboratedSize PackedSize => port.Size.packed;
        public ElaboratedSize? UnpackedSize => port.Size.unpacked;


        public SimplifiedModulePort(ElaboratedModulePort port) => this.port = port;
    }
}
