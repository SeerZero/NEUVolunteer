﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NEUVolunteer.Services
{
    public interface IMottoService
    {
        /// <summary>
        /// 检查更新。
        /// </summary>
        Task<string> CheckUpdateAsync();
    }
}
