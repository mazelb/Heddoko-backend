using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class MaterialType : BaseModel
    {
        [Index(IsUnique = true)]
        [StringLength(50)]
        public string Identifier { get; set; }
    }
}
