using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System.Threading.Tasks;

namespace MlazAPIs.Services.Image_Service
{
    record CloudSetting(string CloudName, string ApiKey , string ApiSecret);
    public record UploadResult(string Url, string PublicId);
    public class ImageUpload
    {
        public readonly IConfiguration _configuration;
        readonly CloudSetting _cloudSetting;
        Cloudinary _cloudinary;

        public ImageUpload(IConfiguration configuration)
        {
            _configuration = configuration;
            _cloudSetting = _configuration.GetSection("CloudinarySettings").Get<CloudSetting>() ?? throw new Exception("Cloudinary settings not found in configuration.");
            var account = new Account(_cloudSetting.CloudName , _cloudSetting.ApiKey, _cloudSetting.ApiSecret);
            _cloudinary = new Cloudinary(account);
        }
        public async Task<UploadResult> ImageUploadAsync(IFormFile image , string folder = null!)
        {
            var imageUpload = new ImageUploadResult();
            if(image is not null && image.Length > 0 )
            {
                using(var imageStream = image.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams
                    {
                        File = new FileDescription(image.FileName, imageStream),
                        Folder = folder
                    };
                    imageUpload = await _cloudinary.UploadAsync(uploadParams);
                }
            }
            return new UploadResult(imageUpload.Url.ToString(), imageUpload.PublicId);
        }
    }
}
