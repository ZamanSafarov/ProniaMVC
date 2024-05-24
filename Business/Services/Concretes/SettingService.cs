using Business.Services.Abstracts;
using Core.Models;
using Core.RepositoryAbstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concretes
{
    public class SettingService : ISettingService
    {
        private readonly ISettingRepository _settingRepository;

        public SettingService(ISettingRepository settingRepository)
        {
            _settingRepository = settingRepository;
        }

        public async Task AddSetting(Setting setting)
        {
           await _settingRepository.AddAsync(setting);
            await _settingRepository.CommitAsync();
        }

        public List<Setting> GetAllSetting(Func<Setting, bool>? func = null)
        {
            return _settingRepository.GetAll(func);
        }

        public Setting GetSetting(Func<Setting, bool>? func = null)
        {
           return _settingRepository.Get(func);
        }

        public async Task<Dictionary<string, string>> GetSettingAsync()
        {
            return await _settingRepository.GetSettingAsync();
        }
    }
}
