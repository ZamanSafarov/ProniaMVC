using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Extensions;

public static partial class Extensions
{
    static public string GetImagePhysicalPath(this IHostEnvironment env, string fileName)
    {
        return Path.Combine(env.ContentRootPath, "wwwroot", "admin", "uploads","sliders", fileName);
    }


    static public void ArchiveImage(this IHostEnvironment env, string fileName)
    {
        var imageActualPath = Path.Combine(env.ContentRootPath,  "wwwroot", "admin", "uploads","sliders", fileName);

        if (File.Exists(imageActualPath))
        {
            var imageNewPath = Path.Combine(env.ContentRootPath, "wwwroot", "admin", "uploads", "sliders", $"archive-{DateTime.Now:yyyyMMddHHmmss}-{fileName}");

            using (FileStream stream = new FileStream(imageActualPath, FileMode.Open))
            {
                using (FileStream newStream = new FileStream(imageNewPath,FileMode.Create))
                {
                    stream.CopyTo(newStream);
                }
            }

            File.Delete(imageActualPath);
        }
        else
        {
            throw new Business.Exceptions.FileNotFoundException($"File does not exsist in {imageActualPath}");
        }
    }

}
