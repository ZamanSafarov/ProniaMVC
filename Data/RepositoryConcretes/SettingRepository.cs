using Core.Models;
using Core.RepositoryAbstracts;
using Data.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.RepositoryConcretes
{
    public class SettingRepository:GenericRepository<Setting>, ISettingRepository
    {
       private readonly AppDbContext _appDbContext;

        public SettingRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Dictionary<string, string>> GetSettingAsync()
        {
            var setting = await _appDbContext.Settings.ToDictionaryAsync(s=>s.Key,s=>s.Value);
            return setting;
        }
    }
}
