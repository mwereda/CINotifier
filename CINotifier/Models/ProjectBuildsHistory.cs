using System.Collections.Generic;
using System.Linq;
using CINotifier.Logic.Projects;

namespace CINotifier.Models
{
    public class ProjectBuildsHistory
    {
        public Project Project { get; set; }
        public IList<Build> Builds { get; set; }

        public bool IsLastSuccess
        {
            get
            {
                return Builds.Any() && Builds.OrderByDescending(x => x.DateAndTime).First().IsSuccess;
            }
        }
    }
}