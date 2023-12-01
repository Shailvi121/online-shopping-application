using System;
using System.Collections.Generic;

namespace Online_Shopping_Application.API.Models;

public partial class UserRole
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public int? RoleId { get; set; }

    public virtual Role? Role { get; set; }

    public virtual UserLogin? User { get; set; }
}
