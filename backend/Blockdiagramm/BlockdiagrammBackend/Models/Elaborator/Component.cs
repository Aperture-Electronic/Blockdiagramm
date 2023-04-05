using BlockdiagrammBackend.Models.Sources;
using BlockdiagrammBackend.Models.Interface;
using HDLElaborateRoslyn.Elaborated;
using System.Security.Cryptography;
using System.Text;
using HDLAbstractSyntaxTree.Types;
using System.Text.Json.Serialization;

namespace BlockdiagrammBackend.Models.Elaborator
{
    [Serializable]
    public class Component
    {
        public string Hash { get; }

        public string Name => Module.Name;

        [JsonIgnore]
        public ElaboratedModule Module { get; private set; }

        [JsonIgnore]
        public SourceFile File { get; }

        public List<ComponentInterface> Interfaces { get; } = new List<ComponentInterface>();

        public int InterfaceCount => Interfaces.Count;

        public int PortCount => Module.ElaboratedModulePorts.Count;

        public Component(SourceFile file, ElaboratedModule module)
        {
            File = file;
            Hash = GetHash(file, module);

            Module = module;
            UpdateInterface();
        }

        public Component UpdateModule(ElaboratedModule module)
        {
            Module = module;

            // TODO
            UpdateInterface();

            // TODO
            GC.Collect();

            return this;
        }

        public static string GetHash(SourceFile file, ElaboratedModule module)
        {
            string fileHashString = file.Hash;
            byte[] fileHash = Convert.FromBase64String(fileHashString);
            byte[] moduleHash = SHA256.HashData(Encoding.UTF8.GetBytes(module.Name));

            int length = moduleHash.Length;
            for (int i = 0; i < length; i++)
            {
                moduleHash[i] += fileHash[i];
            }

            return Convert.ToBase64String(moduleHash);
        }
    
        private void UpdateInterface()
        {
            Interfaces.Clear();
            // TODO: Bus IntelliSense
            // Now we only have single bit "bus"

            // Get all elaborated port of the module
            var elaboratedPorts = Module.ElaboratedModulePorts;

            // This is our bus library, which conatins output (master) and input (slave)
            Bus outputBus = new Bus("output", BusDirection.Master);
            Bus inputBus = new Bus("input", BusDirection.Slave);

            foreach (var port in elaboratedPorts)
            {
                // Determine the bus by port direction
                Bus bus = port.Direction == Direction.In ? inputBus : outputBus;   

                // Create an interface only contains 1 port
                ComponentInterface newInterface = new ComponentInterface(new[] { port }, bus);

                // Add the interface to list
                Interfaces.Add(newInterface);
            }
        }
    }
}
