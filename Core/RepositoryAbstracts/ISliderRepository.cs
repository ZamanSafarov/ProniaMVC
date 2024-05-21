using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using X.PagedList;
using System.Threading.Tasks;

namespace Core.RepositoryAbstracts
{
    public interface ISliderRepository : IGenericRepository<Slider>
    {
        Task<IPagedList<Slider>> GetPaginatedSlidersAsync(int pageIndex, int pageSize);
    }
}
