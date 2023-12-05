

namespace Online_Shopping_Application.API.Models;

public partial class Category
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public int? UpdatedBy { get; set; }

    public bool? IsActive { get; set; }

    public virtual UserLogin? CreatedByNavigation { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual UserLogin? UpdatedByNavigation { get; set; }
}
