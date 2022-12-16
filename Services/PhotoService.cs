using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic;
using RunGroupClubWebApp.Helpers;
using RunGroupClubWebApp.Interfaces;

namespace RunGroupClubWebApp.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly Cloudinary _cloudinary;

        public PhotoService(IOptions<CloudinarySettings> config)
        {
            var acc = new Account(
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret

                ) ;
            _cloudinary = new Cloudinary(acc);

        }
        public async Task<ImageUploadResult> AddPhotoAsync(IFormFile file)
        {
            var UploadResult = new ImageUploadResult();
            if (file.Length > 0)
            {
                using var stream = file.OpenReadStream();
                var UploadParams = new ImageUploadParams
                {
                    File=new FileDescription(file.FileName,stream),
                Transformation=new Transformation().Height(500).Width(500).Crop("fill").Gravity("face")

                };
                UploadResult = await _cloudinary.UploadAsync(UploadParams);
            }
            return UploadResult;
        }

        public async Task<DeletionResult> DeletePhotoAsync(string PublicId)
        {
            var deleteParams=new DeletionParams(PublicId);
            var resulat = await _cloudinary.DestroyAsync(deleteParams);
            return resulat;
        }
    }
}
