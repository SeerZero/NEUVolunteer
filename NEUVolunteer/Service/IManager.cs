using NEUVolunteer.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NEUVolunteer.Service
{
    public interface IManager
    {
        Task Init();

        bool Initialized();
        Task<Manager> Getpassword(string id);


    }
    public static class IManagerConstants
    {
        public const int Version = 4;
        public const string VersionKey = nameof(IManagerConstants) + "." + nameof(Version);
    }
}