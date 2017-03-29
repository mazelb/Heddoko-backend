/**
 * @file License.cs
 * @brief Functionalities required to operate it.
 * @author Sergey Slepokurov (sergey@heddoko.com)
 * @date 11 2016
 * Copyright Heddoko(TM) 2017,  all rights reserved
*/
using System;
using System.Collections.Generic;
using Jil;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace DAL.Models
{
    public class License : BaseModel, IAuditable, ISoftDelete
    {
        public int Amount { get; set; }

        public LicenseStatusType Status { get; set; }

        public DateTime ExpirationAt { get; set; }

        #region Relation

        [JsonIgnore]
        public int? OrganizationID { get; set; }

        [JsonIgnore]
        [JilDirective(Ignore = true)]
        [BsonIgnore]
        public virtual Organization Organization { get; set; }

        [JsonIgnore]
        public int? TeamID { get; set; }

        [JsonIgnore]
        [JilDirective(Ignore = true)]
        [BsonIgnore]
        public virtual Team Team { get; set; }

        [JsonIgnore]
        [JilDirective(Ignore = true)]
        [BsonIgnore]
        public virtual ICollection<User> Users { get; set; }

        #endregion

        #region NotMapped
        [JilDirective(Ignore = true)]
        bool ISoftDelete.IsDeleted => Status == LicenseStatusType.Deleted;

        [JilDirective(Ignore = true)]
        public string IDView => $"LI{Id.ToString(Constants.PadZero)}";

        [JilDirective(Ignore = true)]
        public string ViewID => $"{OrganizationID}-{Id}";

        [JilDirective(Ignore = true)]
        public string Name => $"{IDView} ({ExpirationAt.ToString("dd/MM/yyyy")})";

        [JilDirective(Ignore = true)]
        public bool IsActive => Status == LicenseStatusType.Active && ExpirationAt >= DateTime.Now;

        //TODO remove that later
        public bool Validate()
        {
            if (ExpirationAt > DateTime.Now)
            {
                if (Status == LicenseStatusType.Expired)
                {
                    Status = LicenseStatusType.Active;
                    return true;
                }
                return false;
            }

            if (Status != LicenseStatusType.Active
                &&
                Status != LicenseStatusType.Inactive)
            {
                return false;
            }

            Status = LicenseStatusType.Expired;

            return true;
        }

        #endregion
    }
}