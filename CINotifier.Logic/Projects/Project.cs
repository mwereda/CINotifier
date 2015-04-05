using System.ComponentModel.DataAnnotations;
using EnsureThat;

namespace CINotifier.Logic.Projects
{
    public class Project
    {
        [Required]
        public string Name { get; private set; }

        public Project(string name)
        {
            Ensure.That(name).IsNotNullOrEmpty();

            Name = name;
        }
    }
}
