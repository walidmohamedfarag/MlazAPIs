namespace MlazAPIs.Services.Image_Service
{
    public interface IImageUpload
    {
        Task<UploadResult> ImageUploadAsync(IFormFile image, string folder = null!);
    }
}
