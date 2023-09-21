namespace Resturant.Interfaces
{
    public interface IImageServices
    {
        Task<string> UploadImage(IFormFile Image);
    }
}
