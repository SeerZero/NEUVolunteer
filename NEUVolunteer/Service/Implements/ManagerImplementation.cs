using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace NEUVolunteer.Service.Implements
{
    public class VolunteerImplementation : IVolunteer
    {
        private const string dbname = "NEUVolunteer.NEUVolunteer.db";
        private static readonly string volunteerdbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), dbname);
        private SQLiteAsyncConnection _connection;
        private SQLiteAsyncConnection Connection =>
            _connection ??
            (_connection = new SQLiteAsyncConnection(volunteerdbpath));







        //Console.WriteLine("NEUVolunteer.db");
        public async Task Init()
        {
            using (var dbFileStream = new FileStream(volunteerdbpath, FileMode.OpenOrCreate))
            {
                using (var dbAssetStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(dbname))
                {
                    await dbAssetStream.CopyToAsync(dbFileStream);
                }
            }
            Preferences.Set(IVolunteerConstants.VersionKey, IVolunteerConstants.Version);
        }

        public bool Initialized() =>
            Preferences.Get(IVolunteerConstants.VersionKey, defaultValue: -1) == IVolunteerConstants.Version;
        public async Task<Volunteer> Getpassword(string studentid) =>
             await Connection.Table<Volunteer>().FirstOrDefaultAsync(p => p.id == studentid);




        //await Connection.QueryAsync<Volunteer>("select password)
    }
}