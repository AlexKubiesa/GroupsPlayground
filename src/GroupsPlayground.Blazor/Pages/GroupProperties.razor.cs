using System.ComponentModel.DataAnnotations;

namespace GroupsPlayground.Blazor.Pages
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
