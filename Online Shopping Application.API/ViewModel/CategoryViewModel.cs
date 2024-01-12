using System.ComponentModel.DataAnnotations.Schema;

namespace Online_Shopping_Application.API.ViewModel
{
    public class CategoryViewModel
    {
        

        public string? Name { get; set; }

      
        public bool? IsActive { get; set; }
    }
}
