using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GymBooking.Models {
    public class GymClass {
        public int Id { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        [Required]
       //  [Display(Name = "Passets längd (h:mm)")]
       // [DataType(DataType.Time)] // Gör att Duration-värdet inte visas i Edit-mode...
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:h\\:mm}")]
        public TimeSpan Duration { get; set; }
        public string Description { get; set; }
        public DateTime EndTime { get { return StartTime + Duration; } }

        public virtual ICollection<ApplicationUser> AttendingMembers { get; set; }
    }
}