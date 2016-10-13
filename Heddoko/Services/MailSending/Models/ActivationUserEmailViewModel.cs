namespace Services.MailSending.Models
{
    public class ActivationUserEmailViewModel : EmailViewModel
    {
        public string FirstName { get; set; }

        public string Token { get; set; }
    }
}