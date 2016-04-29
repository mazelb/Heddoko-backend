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
    public class Material : BaseModel
    {
        [Index(IsUnique = true)]
        [StringLength(255)]
        public string Name { get; set; }

        [Index(IsUnique = true)]
        [StringLength(255)]
        public string PartNo { get; set; }

        #region Relations
        public int MaterialTypeID { get; set; }

        public virtual MaterialType MaterialType { get; set; }
        #endregion
    }
}
