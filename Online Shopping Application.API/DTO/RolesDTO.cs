
namespace Online_Shopping_Application.API.DTO
{
    public class RolesDTO
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}
