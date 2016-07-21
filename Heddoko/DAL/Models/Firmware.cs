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
    public class Firmware : BaseModel
    {
        [StringLength(255)]
        public string Version { get; set; }

        public FirmwareType Type { get; set; }

        public FirmwareStatus Status { get; set; }

        [StringLength(255)]
        public string Url { get; set; }
    }
}
