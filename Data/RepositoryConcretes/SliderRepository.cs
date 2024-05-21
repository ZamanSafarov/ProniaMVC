using Core.Models;
using Core.RepositoryAbstracts;
using Data.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace Data.RepositoryConcretes
{
    public class SliderRepository : GenericRepository<Slider>,ISliderRepository
    {
        private readonly AppDbContext _appDbContext;
        public SliderRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<IPagedList<Slider>> GetPaginatedSlidersAsync(int pageIndex, int pageSize)
        {
            var query = _appDbContext.Sliders.AsQueryable();
            return await query.ToPagedListAsync(pageIndex, pageSize);
        }
    }
}
