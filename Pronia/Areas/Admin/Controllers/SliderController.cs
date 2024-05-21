using Business.Exceptions;
using Business.Services.Abstracts;
using Business.Services.Concretes;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Pronia.Areas.Admin.Controllers
{
    [Area("Admin")]
	[Authorize(Roles = "SuperAdmin")]
	public class SliderController : Controller
    {
        private readonly ISliderService _sliderService;

        public SliderController(ISliderService sliderService)
        {
            _sliderService = sliderService;
        }

        public IActionResult Index()
        {
            var sliders = _sliderService.GetAllSliders();
            return View(sliders);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Slider slider)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                await _sliderService.AddSlider(slider);
            }
            catch (ImageExtensionException ex)
            {
                ModelState.AddModelError("ImageFile", ex.Message);
                return View();
            }
            catch (FileNullReferenceException ex)
            {
                ModelState.AddModelError("ImageFile", ex.Message);
                return View();
            }
            catch(Exception ex) {
            return BadRequest(ex.Message);
            }

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            try
            {
                _sliderService.DeleteSlider(id);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound();
            }
            catch(Business.Exceptions.FileNotFoundException ex)
            {
                return NotFound();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Update(int id) 
        {
           var existSlider=  _sliderService.GetSlider(x => x.Id == id && x.DeletedDate == null);
            if (existSlider is null)
            {
                return NotFound();
            }
            return View(existSlider);
        }
        [HttpPost]
        public IActionResult Update(Slider slider)
        {
            if (slider is null)
            {
                return NotFound();
            }
            else if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                _sliderService.UpdateSlider(slider.Id, slider);
            }
            catch (ImageExtensionException ex)
            {
                ModelState.AddModelError("ImageFile", ex.Message);
                return View();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound();
            }
            catch (Business.Exceptions.FileNotFoundException ex)
            {
                ModelState.AddModelError("ImageFile", ex.Message);
                return View();
            }
            catch (FileNullReferenceException ex)
            {
                ModelState.AddModelError("ImageFile", ex.Message);
                return View();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


            return RedirectToAction("Index");

        }

    }
}
