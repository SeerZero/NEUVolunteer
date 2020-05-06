using System;
using System.Collections.Generic;
using System.Text;

namespace NEUVolunteer.Models
{
    [SQLite.Table("Volunteer")]
    public class Volunteer
    {
        [SQLite.Column("VolunteerId")]
        public int VolunteerId { get; set; }

        [SQLite.Column("VolunteerName")]
        public string VolunteerName { get; set; }

        [SQLite.Column("VolunteerSex")]
        public string VolunteerSex { get; set; }

        [SQLite.Column("VolunteerPhone")]
        public long VolunteerPhone { get; set; }

        [SQLite.Column("VolunteerQQ")]
        public long VolunteerQQ { get; set; }

        [SQLite.Column("VolunteerSchoolId")]
        public int VolunteerSchoolId { get; set; }

        [SQLite.Column("VolunteerStudentId")]
        public string VolunteerStudentId { get; set; }

        [SQLite.Column("VolunteerPassword")]
        public string VolunteerPassword { get; set; }

    }
}
