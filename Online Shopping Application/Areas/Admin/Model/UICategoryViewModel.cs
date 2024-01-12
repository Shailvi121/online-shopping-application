
using System.ComponentModel.DataAnnotations.Schema;

namespace Online_Shopping_Application.Areas.Admin.Model
{
    public class UICategoryViewModel
    {
 
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string? Name { get; set; }
    }
}
