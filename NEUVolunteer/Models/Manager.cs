using System;
using System.Collections.Generic;
using System.Text;

namespace NEUVolunteer.Models
{
    [SQLite.Table("Manager")]
    public class Manager
    {
        [SQLite.Column("ManagerId")]
        public int ManagerId { get; set; }

        [SQLite.Column("ManagerName")]
        public string ManagerName { get; set; }

        [SQLite.Column("ManagerPhone")]
        public int ManagerPhone { get; set; }

        [SQLite.Column("ManagerQQ")]
        public long ManagerQQ { get; set; }

        [SQLite.Column("ManagerDepartmentId")]
        public long ManagerDepartmentId { get; set; }

        [SQLite.Column("ManagerAccount")]
        public string ManagerAccount { get; set; }

        [SQLite.Column("ManagerPassword")]
        public string ManagerPassword { get; set; }

    }
}
