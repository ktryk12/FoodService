namespace FoodService.DAL.Interfaces
{
    public interface IImageData
    {
        Task<string> SaveImageAsync(IFormFile file);
        Task<bool> DeleteImageAsync(string imageUrl);
    }

}
