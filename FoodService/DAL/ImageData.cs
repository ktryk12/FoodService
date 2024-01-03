using Microsoft.AspNetCore.Http;
using FoodService.DAL.Interfaces;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using SixLabors.ImageSharp.Formats.Gif;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Advanced;

namespace FoodService.DAL
{
    public class ImageData : IImageData
    {
        private readonly string _imagesPath;

        public ImageData(IWebHostEnvironment env)
        {
            _imagesPath = Path.Combine(env.ContentRootPath, "Images");
            if (!Directory.Exists(_imagesPath))
            {
                Directory.CreateDirectory(_imagesPath);
            }
        }
        public async Task<string> SaveImageAsync(IFormFile file)
        {
            var fileName = GenerateUniqueFileName(file.FileName);
            var filePath = Path.Combine(_imagesPath, fileName);

            if (file.Length > 0)
            {
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    // Decode image from the input stream
                    var image = await Image.LoadAsync(file.OpenReadStream());

                    // Resize the image to a specific width and height while preserving the aspect ratio
                    image.Mutate(x => x.Resize(new ResizeOptions
                    {
                        Mode = ResizeMode.Max,
                        Size = new Size(200, 200) // Replace with your desired size
                    }));

                    // Save the image back to the file system with the appropriate encoder
                    var extension = Path.GetExtension(fileName).ToLower();
                    IImageEncoder encoder = extension switch
                    {
                        ".jpg" or ".jpeg" => new JpegEncoder(),
                        ".png" => new PngEncoder(),
                        ".gif" => new GifEncoder(),
                        _ => throw new InvalidOperationException("Unsupported file extension.")
                    };

                    await image.SaveAsync(fileStream, encoder);
                }
            }

            return $"/Images/{fileName}";
        }





        public async Task<bool> DeleteImageAsync(string imageUrl)
        {
            var filePath = Path.Combine(_imagesPath, imageUrl.TrimStart('/'));

            if (File.Exists(filePath))
            {
                try
                {
                    File.Delete(filePath);
                    return true;
                }
                catch
                {
                    // Log error or handle exception
                    return false;
                }
            }

            return false;
        }

        private string GenerateUniqueFileName(string originalFileName)
        {
            var extension = Path.GetExtension(originalFileName);
            var uniqueName = Guid.NewGuid().ToString();
            return $"{uniqueName}{extension}";
        }
    }
}

