using System;
using System.Collections.Generic;
using System.Text;

namespace NEUVolunteer.Model
{
    [SQLite.Table("ActivityType")]
    public class ActivityType
    {
        [SQLite.Column("TypeId")]
        public int TypeId { get; set; }

        [SQLite.Column("TypeName")]
        public string TypeName { get; set; }
    }
}