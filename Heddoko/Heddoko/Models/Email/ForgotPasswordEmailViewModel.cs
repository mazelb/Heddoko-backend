namespace Heddoko.Models
{
    public class ForgotPasswordEmailViewModel : EmailViewModel
    {
        public string ForgotToken { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }
    }
}