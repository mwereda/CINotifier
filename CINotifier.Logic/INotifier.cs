using System.Collections.Generic;
using CINotifier.Logic.Projects;

namespace CINotifier.Logic
{
    public interface INotifier
    {
        void Initialize();
        void Store();
        void ReportBuildToProject(Build build, Project project);
        IList<ProjectBag> GetProjects();
        IList<Build> GetBuildsForProject(Project project);
        void Delete(string projectName);
    }
}