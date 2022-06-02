using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiServer
{
    public class InAppfileServer
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public InAppfileServer(IWebHostEnvironment webHostEnvironment)
        {
            this._webHostEnvironment = webHostEnvironment;
        }

        public async Task Save(IFormFile file , string folderName)
        {
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var route = Path.Combine(_webHostEnvironment.WebRootPath, folderName);
            if(!Directory.Exists(route))
            {
                Directory.CreateDirectory(route);
            }

            string fileRoute = Path.Combine(route, fileName);
            using (FileStream fileStream = File.Create(fileRoute))
            {
               await file.OpenReadStream().CopyToAsync(fileStream);
            }
        }
    }
}
