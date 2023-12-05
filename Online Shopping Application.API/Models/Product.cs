

namespace Online_Shopping_Application.API.Models;

public partial class Product
{
    public int Id { get; set; }

    public int? CategoryId { get; set; }

    public string? Description { get; set; }

    public string? Image { get; set; }

    public DateTime? CreatedOn { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public int? UpdatedBy { get; set; }

    public bool? IsActive { get; set; }

    public virtual Category? Category { get; set; }

    public virtual UserLogin? CreatedByNavigation { get; set; }

    public virtual UserLogin? UpdatedByNavigation { get; set; }
}
