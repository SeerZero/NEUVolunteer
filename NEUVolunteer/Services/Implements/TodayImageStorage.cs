using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NEUVolunteer.Models;
using NEUVolunteer.Services;
using Xamarin.Essentials;

namespace NEUVolunteer.Services.Implements
{
    public class TodayImageStorage : ITodayImageStorage
    {

        private static readonly TodayImage ForName = new TodayImage();

        private static readonly string FullStartDateKey =
            nameof(TodayImage) + "." + nameof(ForName.FullStartDate);

        private const string FullStartDateDefault = "201901010700";

        private static readonly string ExpiresAtKey =
            nameof(TodayImage) + "." + nameof(ForName.ExpiresAt);

        private static readonly DateTime ExpiresAtDefault =
            new DateTime(2019, 1, 2, 7, 0, 0);

        private static readonly string CopyrightKey =
            nameof(TodayImage) + "." + nameof(ForName.Copyright);

        private const string CopyrightDefault =
            "Salt field province vietnam work (© Quangpraha/Pixabay)";

        private static readonly string CopyrightLinkKey =
            nameof(TodayImage) + "." + nameof(ForName.CopyrightLink);

        private const string CopyrightLinkDefault =
            "https://pixabay.com/photos/salt-field-province-vietnam-work-3344508/";

        /// <summary>
        /// 今日图片文件名。
        /// </summary>
        private const string FileName = "todayImage.bin";

        /// <summary>
        /// 今日图片文件路径。
        /// </summary>
        private static readonly string TodayImagePath =
            Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder
                    .LocalApplicationData), FileName);


        /// <summary>
        /// 获得今日图片。
        /// </summary>
        /// <param name="includingImageStream">包含图片流。</param>
        public async Task<TodayImage> GetTodayImageAsync(
            bool includingImageStream)
        {
            var todayImage = new TodayImage
            {
                FullStartDate =
                    Preferences.Get(FullStartDateKey, FullStartDateDefault),
                ExpiresAt = Preferences.Get(ExpiresAtKey, ExpiresAtDefault),
                Copyright = Preferences.Get(CopyrightKey, CopyrightDefault),
                CopyrightLink =
                    Preferences.Get(CopyrightLinkKey, CopyrightLinkDefault)
            };

            if (!File.Exists(TodayImagePath))
            {
                using (var imageFileStream =
                    new FileStream(TodayImagePath, FileMode.OpenOrCreate))
                using (var imageAssetStream = Assembly.GetExecutingAssembly()
                    .GetManifestResourceStream("NEUVolunteer." + FileName))
                {
                    await imageAssetStream.CopyToAsync(imageFileStream);
                }
            }

            if (!includingImageStream) return todayImage;

            var imageMemoryStream = new MemoryStream();
            using (var imageFileStream =
                new FileStream(TodayImagePath, FileMode.Open))
            {
                await imageFileStream.CopyToAsync(imageMemoryStream);
            }

            todayImage.ImageBytes = imageMemoryStream.GetBuffer();


            return todayImage;
        }

        /// <summary>
        /// 更新今日图片。
        /// </summary>
        /// <param name="todayImage">待更新的今日图片。</param>
        public async Task UpdateTodayImageAsync(TodayImage todayImage)
        {
            Preferences.Set(ExpiresAtKey, todayImage.ExpiresAt);
            if (todayImage.Scope == TodayImage.ModificationScope.ExpiresAt)
                return;

            Preferences.Set(FullStartDateKey, todayImage.FullStartDate);
            Preferences.Set(CopyrightKey, todayImage.Copyright);
            Preferences.Set(CopyrightLinkKey, todayImage.CopyrightLink);

            using (var imageFileStream =
                new FileStream(TodayImagePath, FileMode.Create))
            {
                await imageFileStream.WriteAsync(todayImage.ImageBytes, 0,
                    todayImage.ImageBytes.Length);
            }
        }
    }
}