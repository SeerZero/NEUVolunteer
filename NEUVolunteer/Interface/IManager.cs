using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NEUVolunteer.Interface
{
    interface IManager
    {
        Task Init();

        bool Initialized();
        Task<Manager> Getpassword(string id);


    }
    public static class IManagerConstants
    {
        public const int Version = 1;
        public const string VersionKey = nameof(IManagerConstants) + "." + nameof(Version);
    }
}
