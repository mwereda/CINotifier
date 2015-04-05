using System;
using System.ComponentModel.DataAnnotations;

namespace CINotifier.Logic.Projects
{
    public class Build
    {
        [Required]
        public DateTime DateAndTime { get; set; }

        [Required]
        public Developer Developer { get; set; }

        [Required]
        public string Number { get; set; }

        [Required]
        public bool IsSuccess { get; set; }
    }
}
