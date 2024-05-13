using Business.Exceptions;
using Core.Models;
using Core.RepositoryAbstracts;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Extensions;

public static partial class Extensions
{
    static public void SliderImageRootAdd(this IHostEnvironment env, Slider slider) 
    {
        var extension = Path.GetExtension(slider.ImageFile.FileName);//.png||.jpg
        if (extension != ".png" && extension != ".jpg")
            throw new ImageExtensionException("Image Type Should Be '.png' or '.jpg'");

        var fileName = $"slider-{Guid.NewGuid().ToString().ToLower()}{extension}";


        string path = env.GetImagePhysicalPath(fileName);


        using (var fs = new FileStream(path, FileMode.Create))
        {
             slider.ImageFile.CopyTo(fs);
        }
        slider.ImageUrl = fileName;
    }
}
