using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstracts
{
    public interface ISettingService
    {
        Task AddSetting(Setting setting);
        Setting GetSetting(Func<Setting,bool>? func=null);
        List<Setting> GetAllSetting(Func<Setting,bool>? func=null);

        Task<Dictionary<string, string>> GetSettingAsync();
    }
}
