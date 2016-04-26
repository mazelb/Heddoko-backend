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
    public class Movement : BaseModel
    {
        [StringLength(255)]
        public string Title { get; set; }

        [Column(TypeName = "ntext")]
        public string Notes { get; set; }

        [Column(TypeName = "ntext")]
        public string Data { get; set; }

        public bool? Score { get; set; }

        public bool? ScoreMin { get; set; }

        public bool? ScroreMax { get; set; }

        #region Relations
        public int? StartFrameID { get; set; }

        [JsonIgnore]
        public virtual MovementFrame StartFrame { get; set; }

        public int? EndFrameID { get; set; }

        [JsonIgnore]
        public virtual MovementFrame EndFrame { get; set; }

        public int? ComplexEquipmentID { get; set; }

        [JsonIgnore]
        public virtual ComplexEquipment ComplexEquipment { get; set; }

        public int? SubmittedByID { get; set; }

        [JsonIgnore]
        public virtual User SubmittedBy { get; set; }

        public int? ProfileID { get; set; }

        [JsonIgnore]
        public virtual Profile Profile { get; set; }

        public int? ScreeningID { get; set; }

        [JsonIgnore]
        public virtual Screening Screening { get; set; }

        public int? FolderID { get; set; }

        [JsonIgnore]
        public virtual Folder Folder { get; set; }

        [JsonIgnore]
        public virtual ICollection<MovementTag> Tags { get; set; }

        [JsonIgnore]
        public virtual ICollection<MovementEvent> Events { get; set; }

        [JsonIgnore]
        public virtual ICollection<MovementFrame> Frames { get; set; }

        [JsonIgnore]
        public virtual ICollection<MovementMarker> Markers { get; set; }
        #endregion
    }
}
