using HDLElaborateRoslyn.Elaborated;
using System.Text.Json.Serialization;

namespace BlockdiagrammBackend.Models.Interface
{
    [Serializable]
    public class ComponentInterface
    {
        public Bus? Bus { get; }

        [JsonIgnore]
        public List<ElaboratedModulePort> Ports { get; } = new List<ElaboratedModulePort>();

        /// <summary>
        /// The list of simplified module port, for transmit data between frontend and backend using Json
        /// </summary>
        public IEnumerable<SimplifiedModulePort> ModulePort
            => from port in Ports select new SimplifiedModulePort(port);

        public bool Invalid => !Ports.Any();

        public int PortCount => Ports.Count;

        public ComponentInterface(IEnumerable<ElaboratedModulePort> ports, Bus? bus = null)
        {
            Ports.AddRange(ports);
            Bus = bus;
        }

        public ComponentInterface()
        {
            Bus = null;
        }
    }
}
