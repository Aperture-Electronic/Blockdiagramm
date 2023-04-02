using BlockdiagrammBackend.Models.Sources;

namespace BlockdiagrammBackend.Models.Project
{
    public partial class ProjectInstance
    {
        public void AddSource(SourceFile sourceFile)
        {
            // Check the project valid or not
            if (!IsValid)
            {
                throw new Exception("The project is not opened");
            }

            // Check the source valid or not
            if (!File.Exists(sourceFile.FilePath))
            {
                throw new FileNotFoundException("The source file is not exist", sourceFile.FilePath);
            }

            // Check the hash value in the file list
            var query = from file in SourceFiles
                        where file.Hash.Equals(sourceFile.Hash)
                        select file;

            if (query.Any())
            {
                throw new Exception("The source file is already in the list");
            }

            // Add the file to the list
            SourceFiles.Add(sourceFile);
        }
    }
}
