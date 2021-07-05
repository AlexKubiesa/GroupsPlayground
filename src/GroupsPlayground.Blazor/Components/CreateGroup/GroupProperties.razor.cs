using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GroupsPlayground.Blazor.Components.CreateGroup
{
    public class GroupPropertiesModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [Range(1, 6, ErrorMessage = "Size must be between 1 and 6.")]
        public int Size { get; set; }
    }
}
