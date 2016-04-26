using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class BaseModel
    {
        public int ID { get; set; }

        [JsonIgnore]
        public DateTime? Updated { get; set; }

        [Required, DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [JsonIgnore]
        public DateTime Created { get; set; }

        //inject site URL
        public static string AssetsServer { get; set; }
    }
}
