using Azure.Identity;
using Business.Exceptions;
using Business.Extensions;
using Business.Services.Abstracts;
using Core.Models;
using Core.RepositoryAbstracts;
using Data.RepositoryConcretes;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Business.Services.Concretes
{
    public class SliderService : ISliderService
    {
        private readonly ISliderRepository _sliderRepository;
        private readonly IHostEnvironment _env;

        public SliderService(ISliderRepository sliderRepository, IHostEnvironment env)
        {
            _sliderRepository = sliderRepository;
            _env = env;
        }
        public async Task AddSlider(Slider slider)
        {
            _env.SliderImageRootAdd(slider);

            await _sliderRepository.AddAsync(slider);
            await _sliderRepository.CommitAsync();
        }

        public void DeleteSlider(int id)
        {
            var slider = _sliderRepository.Get(x => x.Id == id && x.DeletedDate == null);

            if (slider is null)
            {
                throw new EntityNotFoundException("Slider Not Found");
            }
            string fileName = slider.ImageUrl;

            //_env.ArchiveImage(fileName);

           _sliderRepository.Delete(slider);
           slider.DeletedDate = DateTime.UtcNow.AddHours(4);
            _sliderRepository.Commit();

        }

        public List<Slider> GetAllSliders(Func<Slider, bool>? func = null)
        {
            var sliders = _sliderRepository.GetAll(func);
            return sliders;
        }

        public Slider GetSlider(Func<Slider, bool>? func = null)
        {
            var slider = _sliderRepository.Get(func);
            return slider;
        }

        public void UpdateSlider(int id, Slider newSlider)
        {
            Slider oldSlider = _sliderRepository.Get(x => x.Id == id && x.DeletedDate ==null);
            if (oldSlider == null) throw new EntityNotFoundException("Slider does not exsist");


         
            oldSlider.Title = newSlider.Title;
            oldSlider.Description = newSlider.Description;
            oldSlider.Offer = newSlider.Offer;
            _env.SliderImageRootAdd(newSlider);
            oldSlider.ImageUrl= newSlider.ImageUrl;
            oldSlider.RedirectUrl = newSlider.RedirectUrl;

            _sliderRepository.Commit();
        }
    }
}
