using NEUVolunteer.Model;
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
    public class ManagerImplementation : IManager
    {
        private const string dbname = "NEUVolunteer.NEUVolunteer.db";
        private static readonly string volunteerdbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), dbname);
        private SQLiteAsyncConnection _connection;
        private SQLiteAsyncConnection Connection =>
            _connection ??
            (_connection = new SQLiteAsyncConnection(volunteerdbpath));

        public async Task Init()
        {
            using (var dbFileStream = new FileStream(volunteerdbpath, FileMode.OpenOrCreate))
            {
                using (var dbAssetStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(dbname))
                {
                    await dbAssetStream.CopyToAsync(dbFileStream);
                }
            }
            Preferences.Set(IManagerConstants.VersionKey, IManagerConstants.Version);
        }

        public bool Initialized() =>
            Preferences.Get(IManagerConstants.VersionKey, defaultValue: -1) == IManagerConstants.Version;
        public async Task<Manager> Getpassword(string managerid) =>
             await Connection.Table<Manager>().FirstOrDefaultAsync(p => p.macount == managerid);




        //await Connection.QueryAsync<Volunteer>("select password)
    }
}