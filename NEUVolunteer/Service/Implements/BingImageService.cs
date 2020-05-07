using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using NEUVolunteer.Model;
using NEUVolunteer.Util;
using NEUVolunteer.Service;
//√
namespace NEUVolunteer.Service.Implements
{
    public class BingImageService : ITodayImageService
    {

        private ITodayImageStorage todayImageStorage;
        private IAlertService alertService;

        private const string Server = "必应每日图片服务器";

        /// <summary>
        /// 获得今日图片。
        /// </summary>
        public async Task<TodayImage> GetTodayImageAsync() =>
            await todayImageStorage.GetTodayImageAsync(true);

        /// <summary>
        /// 检查更新。
        /// </summary>
        public async Task<TodayImageServiceCheckUpdateResult>
            CheckUpdateAsync()
        {
            var todayImage = await todayImageStorage.GetTodayImageAsync(false);
            if (todayImage.ExpiresAt > DateTime.Now)
            {
                return new TodayImageServiceCheckUpdateResult
                { HasUpdate = false };
            }

            BingImageOfTheDayImage bingImage;
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage response;
                try
                {
                    response = await httpClient.GetAsync(
                        "https://www.bing.com/HPImageArchive.aspx?format=js&idx=0&n=1&mkt=zh-CN");
                    response.EnsureSuccessStatusCode();
                }
                catch (Exception e)
                {
                    await alertService.ShowAlertAsync(
                        ErrorMessages.HTTP_CLIENT_ERROR_TITLE,
                        ErrorMessages.HttpClientErrorMessage(Server, e.Message),
                        ErrorMessages.HTTP_CLIENT_ERROR_BUTTON);
                    return new TodayImageServiceCheckUpdateResult
                    { HasUpdate = false };
                }

                var json = await response.Content.ReadAsStringAsync();
                bingImage = JsonConvert
                    .DeserializeObject<BingImageOfTheDay>(json).Images[0];
            }

            var bingImageFullStartDate = DateTime.ParseExact(
                bingImage.Fullstartdate, "yyyyMMddHHmm",
                CultureInfo.InvariantCulture);
            var todayImageFullStartDate = DateTime.ParseExact(
                todayImage.FullStartDate, "yyyyMMddHHmm",
                CultureInfo.InvariantCulture);

            if (bingImageFullStartDate <= todayImageFullStartDate)
            {
                todayImage.ExpiresAt = DateTime.Now.AddHours(2);
                todayImage.Scope = TodayImage.ModificationScope.ExpiresAt;
                await todayImageStorage.UpdateTodayImageAsync(todayImage);
                return new TodayImageServiceCheckUpdateResult
                { HasUpdate = false };
            }

            todayImage = new TodayImage
            {
                FullStartDate = bingImage.Fullstartdate,
                ExpiresAt = bingImageFullStartDate.AddDays(1),
                Copyright = bingImage.Copyright,
                CopyrightLink = bingImage.Copyrightlink,
                Scope = TodayImage.ModificationScope.All
            };

            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage response;
                try
                {
                    response =
                        await httpClient.GetAsync("https://www.bing.com" +
                            bingImage.Url);
                    response.EnsureSuccessStatusCode();
                }
                catch (Exception e)
                {
                    await alertService.ShowAlertAsync(
                        ErrorMessages.HTTP_CLIENT_ERROR_TITLE,
                        ErrorMessages.HttpClientErrorMessage(Server, e.Message),
                        ErrorMessages.HTTP_CLIENT_ERROR_BUTTON);
                    return new TodayImageServiceCheckUpdateResult
                    { HasUpdate = false };
                }

                todayImage.ImageBytes =
                    await response.Content.ReadAsByteArrayAsync();
            }

            await todayImageStorage.UpdateTodayImageAsync(todayImage);
            return new TodayImageServiceCheckUpdateResult
            { HasUpdate = true, TodayImage = todayImage };
        }


        /******** 公开方法 ********/

        public BingImageService(ITodayImageStorage todayImageStorage,
            IAlertService alertService)
        {
            this.todayImageStorage = todayImageStorage;
            this.alertService = alertService;
        }

        /******** 私有方法 ********/

        public class BingImageOfTheDay
        {
            public List<BingImageOfTheDayImage> Images { get; set; }
        }

        public class BingImageOfTheDayImage
        {
            public string Startdate { get; set; }
            public string Fullstartdate { get; set; }
            public string Enddate { get; set; }
            public string Url { get; set; }
            public string Copyright { get; set; }
            public string Copyrightlink { get; set; }
        }
    }
}