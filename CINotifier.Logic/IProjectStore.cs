using System.Collections.Generic;
using CINotifier.Logic.Projects;

namespace CINotifier.Logic
{
    public interface IProjectStore
    {
        void Save(List<ProjectBag> projects);
        IList<ProjectBag> Load();
    }
}