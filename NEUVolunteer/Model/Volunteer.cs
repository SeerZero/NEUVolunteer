using System;
using System.Collections.Generic;
using System.Text;

namespace NEUVolunteer

{
    [SQLite.Table("Volunteer")]
    public class Volunteer
    {
        [SQLite.Column("VolunteerId")]
        public int vid { get; set; }

        [SQLite.Column("VolunteerName")]
        public string name { get; set; }

        [SQLite.Column("VolunteerSex")]
        public string sex { get; set; }
        [SQLite.Column("VolunteerPhone")]
        public string phone { get; set; }

        [SQLite.Column("VolunteerQQ")]
        public string qq { get; set; }

        [SQLite.Column("VolunteerSchoolId")]
        public int sid { get; set; }

        [SQLite.Column("VolunteerStudentId")]
        public string id { get; set; }

        [SQLite.Column("VolunteerPassword")]
        public string password { get; set; }




    }
}