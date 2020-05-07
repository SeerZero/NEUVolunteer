using System;
using System.Collections.Generic;
using System.Text;

namespace NEUVolunteer.Models
{
    [SQLite.Table("News")]
    public class News
    {
        [SQLite.Column("NewsId")]
        public int NewsId { get; set; }

        [SQLite.Column("NewsTitle")]
        public string NewsTitle { get; set; }

        [SQLite.Column("NewsTime")]
        public string NewsTime { get; set; }

        [SQLite.Column("NewsEditor")]
        public string NewsEditor { get; set; }


        [SQLite.Column("NewsContent")]
        public string NewsContent { get; set; }

    }
}
