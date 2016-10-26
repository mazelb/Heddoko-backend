using DAL.Models;

namespace DAL.Helpers
{
    public static class ChanelHelper
    {
        public static string GetChannelName(User user)
        {
            return $"team-{user.TeamID}-{user.Kit?.Id}";
        }
    }
}
