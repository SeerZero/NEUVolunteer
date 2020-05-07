using System;
using System.Collections.Generic;
using System.Text;

namespace NEUVolunteer.Model
{
    [SQLite.Table("Manager")]
    public class Manager
    {

        [SQLite.Column("ManagerId")]
        public int mid { get; set; }

        [SQLite.Column("ManagerName")]
        public string name { get; set; }


        [SQLite.Column("ManagerPhone")]
        public string phone { get; set; }

        [SQLite.Column("ManagerQQ")]
        public string qq { get; set; }

        [SQLite.Column("ManagerDepartmentId")]
        public int id { get; set; }

        [SQLite.Column("ManagerAccount")]
        public string macount { get; set; }

        [SQLite.Column("ManagerPassword")]
        public string password { get; set; }

    }

}