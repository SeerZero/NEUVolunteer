using System;
using System.Collections.Generic;
using System.Text;

namespace NEUVolunteer.Util
{
    public static class ErrorMessages
    {
        public const string HTTP_CLIENT_ERROR_TITLE = "连接错误";

        public static string HttpClientErrorMessage(string server,
            string message) => string.Format($"与{server}连接时发生了错误：\n{message}");

        public const string HTTP_CLIENT_ERROR_BUTTON = "确定";
    }
}
