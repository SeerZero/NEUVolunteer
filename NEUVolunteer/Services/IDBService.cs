using System.Collections.Generic;
using System.Threading.Tasks;
using NEUVolunteer.Models;

namespace NEUVolunteer.Services
{
    public interface IDBService {

        bool Initialized();

        Task InitializeAsync();

        Task<IList<ActivityType>> GetActivityTypesAsync();

        Task<string> GetActivityTypeNameAsync(int id);

        Task AddApplyAsync(int applyActivityId, int applyManagerId, string gatherTime, string gatherPlace, string startTime, string endTime, int number);

        Task<Apply> GetApplyAsync(int id);

        Task<IList<Apply>> GetMyApplyAsync();

        Task<IList<Apply>> GetApplyListAsync();

        //设置一个Apply的状态为报名截止
        Task StopApplyAsync(int id);

        //设置一个Apply的状态为可以报名
        Task RestoreApplyAsync(int id);

        Task UpdateApplyAsync(Apply apply);

        Task AddActivityInfo(string activityName, string activityPlace, string activityBrief, int typeId);

        Task<IList<ActivityInfo>> GetActivityInfosAsync();

        Task<ActivityInfo> GetActivityInfoAsync(int id);

        Task<Manager> GetManagerAsync(int id);

        Task<Manager> GetManagerAsync(string account);

        Task<Volunteer> GetVolunteerAsync(int id);

        Task<Volunteer> GetVolunteerAsync(string studentId);

        Task<bool> CanVolunteerInApply(int applyId);

        Task AddVolunteerInApply(int applyId, int volunteerId);

        Task DeleteVolunteerInApply(int applyId, int volunteerId);

        Task<bool> IsVolunteerInApply(int applyId, int volunteerId);

        Task<News> GetNewsAsync(int id);

        Task<IList<News>> GetAllNewsAsync();

    }

    public static class DBConstants
    {
        /// <summary>
        /// 版本键。
        /// </summary>
        public const string VersionKey =
            nameof(DBConstants) + "." + nameof(Version);

        /// <summary>
        /// 版本。
        /// </summary>
        public const int Version = 10;
    }
}
