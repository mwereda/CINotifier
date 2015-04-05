using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace CINotifier.Logic.Projects
{
    [JsonObject(IsReference = false)] 
    public class ProjectBag
    {
        private const int BuildsMaxNumber = 5;

        public string Name { get; set; }

        internal BuildsQueue<Build> Builds { get; private set; }

        [JsonIgnore]
        public bool IsLastSuccess
        {
            get { return true; }
        }

        internal ProjectBag()
        {
            Builds = new BuildsQueue<Build>(BuildsMaxNumber);
        }

        public IList<Build> ListBuilds()
        {
            return Builds.AsQueryable().OrderByDescending(x => x.DateAndTime).ToList();
        }
    }
}