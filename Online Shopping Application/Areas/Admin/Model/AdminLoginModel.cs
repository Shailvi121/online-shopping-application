namespace Online_Shopping_Application.Area.Admin.Model
{
    public class AdminLoginModel
    {
        [Key]
        public int Id { get; set; }
        [Required (ErrorMessage="username is necessary")]
        public string?Username{ get; set; }

        [Required(ErrorMessage = "password is necessary")]
        public string? Password { get; set; }
    }
}
