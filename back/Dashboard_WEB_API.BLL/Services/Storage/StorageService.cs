using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard_WEB_API.BLL.Services.Storage
{
    public class StorageService : IStorageService
    {

        public async Task<string?> SaveImageAsync(IFormFile file, string folderPath)
        {
            try
            {
                var types = file.ContentType.Split('/');
                if (types.Length != 2 || types[0] != "image")
                    return null;

                var extension = Path.GetExtension(file.FileName);
                var imageName = $"{Guid.NewGuid()}{extension}";
                var imagePath = Path.Combine(folderPath, imageName);
                using (var stream = File.Create(imagePath))
                {
                    await file.CopyToAsync(stream);
                }
                return imageName;
            }
            catch (Exception)
            {
                return null;
            }
            

        }

        public async Task<IEnumerable<string?>> SaveImagesAsync(IEnumerable<IFormFile> files, string folderPath)
        {
            try
            {
                var tasks = files.Select(file => SaveImageAsync(file, folderPath));
                var results = await Task.WhenAll(tasks);
                return results.Where(result => result != null)!;
            }
            catch (Exception)
            {

                return null;
            }
        }
        public async Task<bool> DeleteAllImagesAsync(string folderPath)
        {
            try
            {
                if (Directory.Exists(folderPath))
                {
                    Directory.Delete(folderPath, true);
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
