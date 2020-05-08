using System;
using System.Collections.Generic;
using System.Text;

namespace NEUVolunteer.Models
{
    public class ApplyDetail
    {
        public ApplyDetail(Apply apply, ActivityInfo info) {
            Apply = apply;
            Info = info;
        }

        public Apply Apply { get; set; }

        public ActivityInfo Info { get; set; }


    }
}
