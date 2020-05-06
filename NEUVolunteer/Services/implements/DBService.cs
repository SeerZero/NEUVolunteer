using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NEUVolunteer.Models;
using NEUVolunteer.Services.interfaces;
using SQLite;

namespace NEUVolunteer.Services.implements
{
    public class DBService : IDBService {

        public DBService(IPreferenceStorage preferenceStorage) {
            _preferenceStorage = preferenceStorage;
        }

        private IPreferenceStorage _preferenceStorage;

        public const string DbName = "NEUVolunteer.db";

        private static readonly string DbPath =
            Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder
                    .LocalApplicationData), DbName);

        private SQLiteAsyncConnection _connection;

        private SQLiteAsyncConnection Connection =>
            _connection ??
            (_connection = new SQLiteAsyncConnection(DbPath));

        public bool Initialized() =>
            _preferenceStorage.Get(DBConstants.VersionKey, -1) ==
            DBConstants.Version;

        public async Task InitializeAsync() {
            using (var dbFileStream =
                new FileStream(DbPath, FileMode.Create))
            using (var dbAssetStream = Assembly.GetExecutingAssembly()
                .GetManifestResourceStream(DbName)) {
                await dbAssetStream.CopyToAsync(dbFileStream);
            }
            
            _preferenceStorage.Set(DBConstants.VersionKey,
                DBConstants.Version);
        }

        public async Task<IList<ActivityType>> GetActivityTypesAsync() =>
            await Connection.Table<ActivityType>().ToListAsync();

        public async Task<string> GetActivityTypeNameAsync(int id) {
            var type = await Connection.Table<ActivityType>().FirstOrDefaultAsync(p => p.TypeId.Equals(id));
            return type.TypeName;
        }

        public async Task AddApplyAsync(int applyActivityId, int applyManagerId, string gatherTime, string gatherPlace, string startTime, string endTime, int number) {
            var sql = "insert into Apply (ApplyActivityId,ApplyManagerId,GatherTime,GatherPlace,StartTime,EndTime,RequestNumber) values ("
                      + applyActivityId.ToString() + "," + applyManagerId.ToString() + ",'" + gatherTime + "','"
                      + gatherPlace + "','" + startTime + "','" + endTime + "'," + number + ")";
            await Connection.QueryAsync<Apply>(sql);
        }

        public async Task<Apply> GetApplyAsync(int id) =>
            await Connection.Table<Apply>().FirstOrDefaultAsync(p => p.ApplyId.Equals(id));

        public async Task AddActivityInfo(string activityName, string activityPlace, string activityBrief, int typeId) {
            var sql = "insert into ActivityInfo (ActivityName,ActivityPlace,ActivityBrief,ActivityTypeId) values ('"
                      + activityName + "','" + activityPlace + "','" + activityBrief + "'," + typeId.ToString() + ")";
            await Connection.QueryAsync<ActivityInfo>(sql);
        }

        public async Task<IList<ActivityInfo>> GetActivityInfosAsync() =>
            await Connection.Table<ActivityInfo>().ToListAsync();

        public async Task<ActivityInfo> GetActivityInfoAsync(int id) =>
            await Connection.Table<ActivityInfo>().FirstOrDefaultAsync(p => p.ActivityId.Equals(id));

        public async Task<Manager> GetManagerAsync(int id) =>
            await Connection.Table<Manager>().FirstOrDefaultAsync(p => p.ManagerId.Equals(id));

        public async Task<Volunteer> GetVolunteerAsync(int id) =>
            await Connection.Table<Volunteer>().FirstOrDefaultAsync(p => p.VolunteerId.Equals(id));

        public async Task AddVolunteerInApply(int applyId, int volunteerId) {
            var sql = "insert into VolunteerInApply values (" + applyId + "," + volunteerId + ")";
            await Connection.QueryAsync<VolunteerInApply>(sql);
        }

        public async Task DeleteVolunteerInApply(int applyId, int volunteerId) {
            var sql = "delete from VolunteerInApply where ApplyId=" + applyId + " and VolunteerId=" + volunteerId;
            await Connection.QueryAsync<VolunteerInApply>(sql);
        }

        public async Task<bool> IsVolunteerInApply(int applyId, int volunteerId) {
            var result = await Connection.Table<VolunteerInApply>()
                .Where(p => p.VolunteerId.Equals(volunteerId) && p.ApplyId.Equals(applyId)).ToListAsync();
            return (result.Count != 0);
        }
    }
}
