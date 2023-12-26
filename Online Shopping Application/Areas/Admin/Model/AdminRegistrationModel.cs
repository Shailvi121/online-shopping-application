namespace Online_Shopping_Application.Area.Admin.Model
{
    public class AdminRegistrationModel
    {

       
        [Required(ErrorMessage = "username is necessary")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "password is necessary")]
        public string? Password { get; set; }
    }
}
