using System;
using System.Collections.Generic;
using System.Text;

namespace NEUVolunteer.Model
{
    [SQLite.Table("ActivityInfo")]
    public class ActivityInfo
    {
        [SQLite.Column("ActivityId")]
        public int ActivityId { get; set; }

        [SQLite.Column("ActivityName")]
        public string ActivityName { get; set; }

        [SQLite.Column("ActivityPlace")]
        public string ActivityPlace { get; set; }

        [SQLite.Column("ActivityBrief")]
        public string ActivityBrief { get; set; }

        [SQLite.Column("ActivityTypeId")]
        public int ActivityTypeId { get; set; }
    }
}