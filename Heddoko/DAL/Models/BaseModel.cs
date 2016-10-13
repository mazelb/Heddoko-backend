using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace DAL.Models
{
    public class BaseModel : IBaseModel
    {
        public int Id { get; set; }

        [JsonProperty("updatedAt")]
        public DateTime? Updated { get; set; }

        [Required, DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [JsonProperty("createdAt")]
        public DateTime Created { get; set; }

        //inject site URL
        public static string AssetsServer { get; set; }
    }
}