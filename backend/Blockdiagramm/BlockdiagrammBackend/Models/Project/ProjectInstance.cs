using BlockdiagrammBackend.Models.Elaborator;
using BlockdiagrammBackend.Models.Sources;
using BlockdiagrammBackend.Session;
using System.Collections.Concurrent;

namespace BlockdiagrammBackend.Models.Project
{
    [Serializable]
    public partial class ProjectInstance
    {
        private readonly object projectGlobalLock = new object();

        public bool IsValid
        {
            get
            {
                lock (projectGlobalLock)
                {
                    return !string.IsNullOrEmpty(Path);
                }
            }
        }

        public string Path { get; private set; }

        public string Name { get; private set; }

        public string SessionId { get; }

        public ConcurrentBag<SourceFile> SourceFiles { get; }

        public ProjectInstance(string sessionId)
        {
            SessionId = sessionId;
            Path = string.Empty;
            Name = string.Empty;
            SourceFiles = new ConcurrentBag<SourceFile>();
        }

        public void NewProject(string projectName, string projectPath)
        {
            lock (projectGlobalLock)
            {
                if (string.IsNullOrWhiteSpace(projectName) || string.IsNullOrWhiteSpace(projectPath))
                {
                    throw new Exception($"{nameof(projectName)} and {nameof(projectPath)} can not be empty");
                }

                Name = projectName;
                Path = projectPath;

                SourceFiles.Clear();
                Components.Clear();
            }
        }

        public void CloseProject()
        {
            lock (projectGlobalLock)
            {
                if (IsValid)
                {
                    Path = string.Empty;
                    Name = string.Empty;
                }
            }
        }

    }
}
