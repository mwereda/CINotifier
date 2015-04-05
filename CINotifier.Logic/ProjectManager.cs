using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CINotifier.Logic.Extenstions;
using CINotifier.Logic.Projects;
using EnsureThat;

namespace CINotifier.Logic
{
    internal class ProjectManager
    {       
        private static ProjectManager manager;

        private readonly List<ProjectBag> projects;

        private ProjectManager()
        {
            this.projects = new List<ProjectBag>();
        }

        internal static ProjectManager Instance
        {
            get
            {
                if (manager.IsNull())
                {
                    manager = new ProjectManager();
                }

                return manager;
            }
        }

        internal void AddBuildToProject(Build build, Project project)
        {
            var projectBag = GetProjectBag(project);
            if (projectBag.IsNull())
            {
                projectBag = new ProjectBag {Name = project.Name};
                this.projects.Add(projectBag);
            }

            projectBag.Builds.Enqueue(build);            
        }

        internal IList<Build> GetBuildsFromProject(Project project)
        {
            var projectBag = GetProjectBag(project);
            if (projectBag.IsNull())
            {
                return null;
            }

            return projectBag.Builds.ToList();
        }

        internal IList<ProjectBag> GetProjects()
        {
            return this.projects;
        }

        internal void Initialize(List<ProjectBag> projectBags)
        {
            Ensure.That(projectBags).IsNotNull();

            this.projects.AddRange(projectBags);
        }

        private ProjectBag GetProjectBag(Project project)
        {
            var projectBag = this.projects.FirstOrDefault(x => x.Name.Equals(project.Name));

            return projectBag;
        }
    }
}
