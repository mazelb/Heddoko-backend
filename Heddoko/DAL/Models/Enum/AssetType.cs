using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public enum AssetType
    {
        [StringValue(Constants.Assets.Seed)]
        Seed = 0,
        [StringValue(Constants.Assets.User)]
        User = 1,
        [StringValue(Constants.Assets.Profile)]
        Profile = 2,
        [StringValue(Constants.Assets.Group)]
        Group = 3,
    }
}
