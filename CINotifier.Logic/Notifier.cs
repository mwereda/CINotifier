using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CINotifier.Logic.Projects;
using EnsureThat;

namespace CINotifier.Logic
{
    public class Notifier : INotifier
    {
        private readonly IProjectStore projectStore;

        public Notifier(IProjectStore projectStore)
        {
            Ensure.That(projectStore).IsNotNull();

            this.projectStore = projectStore;
        }

        public void Initialize()
        {
            var projects = this.projectStore.Load();
            ProjectManager.Instance.Initialize(projects.ToList());
        }

        public void Store()
        {
            var projects = GetProjects();
            this.projectStore.Save(projects.ToList());
        }

        public void ReportBuildToProject(Build build, Project project)
        {
            Ensure.That(build).IsNotNull();
            Ensure.That(project).IsNotNull();

            ProjectManager.Instance.AddBuildToProject(build, project);

            Task.Factory.StartNew(Store);
        }

        public IList<ProjectBag> GetProjects()
        {
            return ProjectManager.Instance.GetProjects();
        }

        public IList<Build> GetBuildsForProject(Project project)
        {
            Ensure.That(project).IsNotNull();

            return ProjectManager.Instance.GetBuildsFromProject(project);
        }
    }
}
