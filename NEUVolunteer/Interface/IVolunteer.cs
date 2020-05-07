using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NEUVolunteer.Interface
{
    interface IVolunteer
    {
        Task Init();

        bool Initialized();
        Task<Volunteer> Getpassword(string id);


    }
    public static class IVolunteerConstants
    {
        public const int Version = 1;
        public const string VersionKey = nameof(IVolunteerConstants) + "." + nameof(Version);
    }
}
