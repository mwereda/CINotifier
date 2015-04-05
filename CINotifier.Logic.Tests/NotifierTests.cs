using System;
using System.Linq;
using CINotifier.Logic.Projects;
using Xunit;
using Xunit.Extensions;

namespace CINotifier.Logic.Tests
{
    public class NotifierTests
    {
        [Theory, AutoNSData]
        public void ReportBuildToProject_NullBuild_ThrowsArgumentNullException(Notifier notifier)
        {
            Assert.Throws<ArgumentNullException>(() => notifier.ReportBuildToProject(null, new Project("notifier")));
        }

        [Theory, AutoNSData]
        public void ReportBuildToProject_NullProject_ThrowsArgumentNullException(Notifier notifier)
        {
            Assert.Throws<ArgumentNullException>(() => notifier.ReportBuildToProject(new Build(), null));
        }

        [Theory, AutoNSData]
        public void ReportBuildToProject_AddingBuildToNotExistingProject_BuildIsAdded(Notifier notifier, Build build,
            Project project)
        {
            notifier.ReportBuildToProject(build, project);

            var builds = notifier.GetBuildsForProject(new Project(project.Name));

            Assert.NotNull(builds);
            Assert.NotEmpty(builds);
            Assert.Equal(build.Number, builds.First().Number);
        }

        [Theory, AutoNSData]
        public void ReportBuildToProject_AddingBuildToExistingProject_BuildIsAdded(Notifier notifier, Build buildOne, Build buildTwo,
            Project project)
        {
            notifier.ReportBuildToProject(buildOne, project);
            notifier.ReportBuildToProject(buildTwo, project);

            var builds = notifier.GetBuildsForProject(new Project(project.Name));

            Assert.NotNull(builds);
            Assert.NotEmpty(builds);
            Assert.Equal(2, builds.Count());
        }

        [Theory, AutoNSData]
        public void ReportBuildToProject_AddingSixBuildsToExistingProject_OnlyFiveBuilds(Notifier notifier, Project project)
        {
            for (int i = 1; i <= 6; i++)
            {
                var build = new Build {Number = i.ToString()};
                notifier.ReportBuildToProject(build, project);
            }

            var builds = notifier.GetBuildsForProject(new Project(project.Name));

            Assert.NotNull(builds);
            Assert.NotEmpty(builds);
            Assert.Equal(5, builds.Count());
            Assert.Equal("6", builds.Last().Number);
        }

        [Theory, AutoNSData]        
        public void GetBuildsForProject_NullProject_ThrowsArgumentNullException(Notifier notifier)
        {
            Assert.Throws<ArgumentNullException>(() => notifier.GetBuildsForProject(null));
        }

        [Theory, AutoNSData]
        public void GetProjects_NoProject_ReturnsNullCollection(Notifier notifier)
        {
            var projects = notifier.GetProjects();

            Assert.NotNull(projects);
            Assert.Empty(projects);
        }
    }
}
