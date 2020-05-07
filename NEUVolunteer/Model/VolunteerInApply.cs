using System;
using System.Collections.Generic;
using System.Text;

namespace NEUVolunteer.Model
{
    [SQLite.Table("VolunteerInApply")]
    public class VolunteerInApply
    {
        public int ApplyId { get; set; }

        public int VolunteerId { get; set; }
    }
}