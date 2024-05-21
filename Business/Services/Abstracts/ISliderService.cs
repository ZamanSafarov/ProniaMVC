﻿using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace Business.Services.Abstracts
{
    public interface ISliderService
    {
        Task AddSlider(Slider slider);
        void DeleteSlider(int id);
        void UpdateSlider(int id, Slider newSlider);
        Slider GetSlider(Func<Slider, bool>? func = null);
        List<Slider> GetAllSliders(Func<Slider, bool>? func = null);
        Task<IPagedList<Slider>> GetPaginatedSlidersAsync(int pageIndex, int pageSize);
    }
}
