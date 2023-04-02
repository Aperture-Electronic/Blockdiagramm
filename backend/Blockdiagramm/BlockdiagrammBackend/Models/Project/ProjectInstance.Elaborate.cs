using BlockdiagrammBackend.Models.Elaborator;
using BlockdiagrammBackend.Models.Sources;
using System.Runtime.CompilerServices;

namespace BlockdiagrammBackend.Models.Project
{
    public partial class ProjectInstance
    {
        private void Elaborate(SourceFile file)
        {
            if (!file.Exist)
            {
                throw new FileNotFoundException($"The file ({file.ShortName}) to elaborate is not exist", file.FilePath);
            }


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
            => Parallel.ForEach(SourceFiles, (file) => Elaborate(file));
    }
}
