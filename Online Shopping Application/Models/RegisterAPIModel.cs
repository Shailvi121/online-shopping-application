namespace Online_Shopping_Application.Models
{
    public class RegisterAPIModel
    {
        [Required(ErrorMessage = "username is necessary")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "password is necessary")]
        public string? Password { get; set; }
    }
}
