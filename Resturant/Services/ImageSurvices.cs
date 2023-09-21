using Microsoft.AspNetCore.Hosting;
using Resturant.Interfaces;

namespace Resturant.Services
{
    public class ImageServices : IImageServices
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ImageServices (IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment=webHostEnvironment;
        }

        public async Task<string> UploadImage(IFormFile Image)
        {
            string folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(Image.FileName);
            string filePath = Path.Combine(folderPath, fileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await Image.CopyToAsync(fileStream);
            }


/*            // Resize
            ResizeImage(fileName);*/

            return fileName;
        }
    }
}
