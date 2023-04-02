using BlockdiagrammBackend.Models.Elaborator;
using BlockdiagrammBackend.Models.Sources;
using HDLAbstractSyntaxTree.HDLElement;
using HDLElaborateRoslyn.Elaborated;
using HDLElaborateRoslyn.Elaborator;
using HDLParserSharp;
using System.Collections.Concurrent;

namespace BlockdiagrammBackend.Models.Project
{
    public partial class ProjectInstance
    {
        private ConcurrentBag<Component> Components { get; } = new();

        private void Elaborate(SourceFile file)
        {
            if (!file.Exist)
            {
                throw new FileNotFoundException($"The file ({file.ShortName}) to elaborate is not exist", file.FilePath);
            }

            // Read the file content and get ready to elaborate
            file.ReadFileContent().Wait();

            // Use the language parser to get abstract language tree (AST)
            HDLEvaluator evaluator = new();
            List<HDLObject> ast = ParserHDLFileByType(file.Content, file.Type, evaluator);

            // Use elaborator to get modules, generics and ports
            HDLElaborator elaborator = new(ast);

            elaborator.ElaborateModules();
            elaborator.GenerateModuleGenericsList();
            elaborator.ElaborateModuleGenerics();

            // Make the component and put them into the components list
            foreach (ElaboratedModule module in elaborator.Modules)
            {
                Component component = new(file, module);
                Components.Add(component);
            }
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

        public void Elaborate(string hash)
        {
            IEnumerable<SourceFile> query = from source in SourceFiles
                                            where source.Hash == hash
                                            select source;

            if (!query.Any())
            {
                throw new Exception("The file to elaborate must be contained in project");
            }

            Elaborate(query.First());
        }

        public void ElaborateAll()
        {
            Components.Clear();
            // Parallel.ForEach(SourceFiles, Elaborate);
            foreach (SourceFile source in SourceFiles)
            {
                Elaborate(source);
            }
        }
    }
}
