using NEUVolunteer.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using NEUVolunteer.Services;

namespace NEUVolunteer.Services.Implements
{
    public class MottoService : IMottoService
    {
        private IAlertService alertService;
        public MottoService(IAlertService alertService)
        {
            this.alertService = alertService;
        }


        string motto = "Let us work without theorizing, tis the only way to make life endurable.";

        /// <summary>
        /// 检查更新。
        /// </summary>
        public async Task<string>
            CheckUpdateAsync()
        {
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage response;
                try
                {
                    response = await httpClient.GetAsync(
                        "http://open.iciba.com/dsapi/");
                    response.EnsureSuccessStatusCode();
                }
                catch (Exception e)
                {
                    await alertService.ShowAlertAsync(
                        ErrorMessages.HTTP_CLIENT_ERROR_TITLE,
                        ErrorMessages.HttpClientErrorMessage("名言服务器", e.Message),
                        ErrorMessages.HTTP_CLIENT_ERROR_BUTTON);
                    return motto;
                }

                var json = await response.Content.ReadAsStringAsync();
                motto = JsonConvert
                    .DeserializeObject<MottoOfTheDay>(json).content;
                return motto;
            }
        }

    }

    /// <summary>
    /// 解析json
    /// </summary>
    public class MottoOfTheDay
    {
        public string content { get; set; }
    }
}
