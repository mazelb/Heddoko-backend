namespace Services.MailSending.Models
{
    public class ForgotPasswordEmailViewModel : EmailViewModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public string ResetPasswordUrl { get; set; }
    }
}