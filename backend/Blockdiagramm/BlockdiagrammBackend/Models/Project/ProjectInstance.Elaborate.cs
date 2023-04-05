using BlockdiagrammBackend.Models.Elaborator;
using BlockdiagrammBackend.Models.Sources;
using HDLAbstractSyntaxTree.HDLElement;
using HDLElaborateRoslyn.Elaborated;
using HDLElaborateRoslyn.Elaborator;
using HDLParserSharp;
using System.Collections.Concurrent;
using System.Runtime.InteropServices;

namespace BlockdiagrammBackend.Models.Project
{
    public partial class ProjectInstance
    {
        public IEnumerable<Component> ComponentsList => Components.Values;

        private ConcurrentDictionary<string, Component> Components { get; } = new();

        private List<string> Elaborate(SourceFile file)
        {
            if (!file.Exist)
            {
                throw new FileNotFoundException($"The file ({file.ShortName}) to elaborate is not exist", file.FilePath);
            }

            // Get old hash
            string oldHash = file.ContentHash;

            // Read the file content and get ready to elaborate
            file.ReadFileContent().Wait();

            // Get new hash
            string newHash = file.ContentHash;

            if (oldHash == newHash)
            {
                // The hash is match, do not update the file
                return new List<string>();
            }

            // Use the language parser to get abstract language tree (AST)
            HDLEvaluator evaluator = new();
            List<HDLObject> ast = ParserHDLFileByType(file.Content, file.Type, evaluator);

            // Use elaborator to get modules, generics and ports
            HDLElaborator elaborator = new(ast);

            elaborator.ElaborateModules();
            elaborator.GenerateModuleGenericsList();
            elaborator.ElaborateModuleGenerics();
            elaborator.ElaborateModulePort();

            // Make the component and put them into the components list

            // Make a hash list to store which component is latest, 
            // and others are need to be deleted
            List<string> updatedOrAdded = new List<string>();
            foreach (ElaboratedModule module in elaborator.Modules)
            {
                // Get the new hash of new elaborated module
                string newComponentHash = Component.GetHash(file, module);
                updatedOrAdded.Add(newComponentHash);

                // If there is a matched hash, replace the component
                // Else, add the new component to the file
                Component newComponent = new Component(file, module);
                Components.AddOrUpdate(newComponent.Hash, (_) => new Component(file, module),
                    (_, original) => original.UpdateModule(module));    
            }

            return updatedOrAdded;
        }

        private static List<HDLObject> ParserHDLFileByType(string content, SourceFileType type, HDLEvaluator evaluator)
        {
            List<HDLObject> ast = new List<HDLObject>();

            switch (type)
            {
                case SourceFileType.SystemVerilogSource:
                    SystemVerilogParserContainer svParser = new(ast, HDLLanguage.SystemVerilog, evaluator.EvalToBool);
                    svParser.ParseString(content);
                    break;
                case SourceFileType.VerilogSource:
                    // TODO
                    break;
                case SourceFileType.VHDLSource:
                    // TODO
                    break;
                case SourceFileType.SystemVerilogHeader:
                    // TODO
                    break;
                default:
                    break;
            }

            return ast;
        }

        private List<ElaborateResult> Elaborate(IEnumerable<SourceFile> sourceFiles)
        {
            List<ElaborateResult> result = new List<ElaborateResult>();
            List<string> updatedOrAdded = new List<string>();
            foreach (SourceFile file in sourceFiles)
            {
                List<string> updateOrAddedInFile;

                try
                {
                    updateOrAddedInFile = Elaborate(file);

                    // Get components name
                    var newComponentsName = from newHash in updateOrAddedInFile
                                            select Components[newHash].Module.Name;
                    result.Add(new ElaborateResult(true, file.Hash, newComponentsName));
                }
                catch (Exception ex)
                {
                    result.Add(new ElaborateResult(false, file.Hash, ex.Message));
                    break;
                }

                foreach (string newHash in updateOrAddedInFile)
                {
                    if (!updatedOrAdded.Contains(newHash))
                    {
                        updatedOrAdded.Add(newHash);
                    }
                }
            }

            List<string> componentHash = Components.Keys.ToList();
            foreach (string hash in componentHash)
            {
                if (!updatedOrAdded.Contains(hash))
                {
                    Components.Remove(hash, out _);
                }
            }

            return result;
        }

        public List<ElaborateResult> Elaborate(string hash)
        {
            IEnumerable<SourceFile> query = from source in SourceFiles
                                            where source.Hash == hash
                                            select source;

            if (!query.Any())
            {
                throw new Exception("The file to elaborate must be contained in project");
            }

            return Elaborate(query);
        }

        public List<ElaborateResult> ElaborateAll() => Elaborate(SourceFiles);
    }
}
