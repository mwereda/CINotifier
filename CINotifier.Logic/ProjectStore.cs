using System.Collections.Generic;
using System.IO;
using System.Web;
using CINotifier.Logic.Infrastructure.Json;
using CINotifier.Logic.Projects;
using Newtonsoft.Json;

namespace CINotifier.Logic
{
    public class ProjectStore : IProjectStore
    {
        private const string FileName = "projects.json";

        private readonly object lockFile = new object();
        private readonly string filePath;

        public ProjectStore()
        {
            this.filePath = Path.Combine(HttpRuntime.AppDomainAppPath, FileName);
        }

        public void Save(List<ProjectBag> projects)
        {
            var serializedProjects = JsonConvert.SerializeObject(projects, GetJsonSerializerSettings());

            lock (lockFile)
            {
                File.WriteAllText(this.filePath, serializedProjects);
            }
        }

        public IList<ProjectBag> Load()
        {
            string projectsText = string.Empty;
            lock (lockFile)
            {
                if (File.Exists(this.filePath))
                {
                    projectsText = File.ReadAllText(this.filePath);
                }
            }

            if (string.IsNullOrEmpty(projectsText))
            {
                return new List<ProjectBag>();
            }

            return JsonConvert.DeserializeObject<List<ProjectBag>>(projectsText, GetJsonSerializerSettings());
        }

        private JsonSerializerSettings GetJsonSerializerSettings()
        {
            return new JsonSerializerSettings { ContractResolver = new InternalContractResolver(), ReferenceLoopHandling = ReferenceLoopHandling.Serialize };
        }
    }
}