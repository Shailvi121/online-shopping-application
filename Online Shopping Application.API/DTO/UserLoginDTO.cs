

namespace Online_Shopping_Application.API.DTO
{
    public class UserLoginDTO
    {
        public int Id { get; set; }

        public string? Username { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Password { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public bool? IsActive { get; set; }

        public virtual ICollection<Category> CategoryCreatedByNavigations { get; set; } = new List<Category>();

        public virtual ICollection<Category> CategoryUpdatedByNavigations { get; set; } = new List<Category>();

        public virtual ICollection<Product> ProductCreatedByNavigations { get; set; } = new List<Product>();

        public virtual ICollection<Product> ProductUpdatedByNavigations { get; set; } = new List<Product>();

        public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}
