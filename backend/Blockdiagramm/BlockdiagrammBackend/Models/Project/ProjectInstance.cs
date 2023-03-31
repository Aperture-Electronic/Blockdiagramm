using BlockdiagrammBackend.Session;

namespace BlockdiagrammBackend.Models.Project
{
    public class ProjectInstance
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

        public ProjectInstance(string sessionId)
        {
            Path = string.Empty;
            Name = string.Empty;
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
