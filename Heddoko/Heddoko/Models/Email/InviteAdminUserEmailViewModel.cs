namespace Heddoko.Models
{
    public class InviteAdminUserEmailViewModel : EmailViewModel
    {
        public string FirstName { get; set; }

        public string OrganizationName { get; set; }

        public string Token { get; set; }
    }
}