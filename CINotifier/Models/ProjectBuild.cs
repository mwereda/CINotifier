using System.ComponentModel.DataAnnotations;
using CINotifier.Logic.Projects;

namespace CINotifier.Models
{
    public class ProjectBuild
    {
        [Required]
        public Project Project { get; set; }

        [Required]
        public Build Build { get; set; }
    }
}