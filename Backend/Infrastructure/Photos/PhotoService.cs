using Application.Interfaces;
using Application.Profiles.DTOs;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Photos;

public class PhotoService : IPhotoService
{
    private readonly Cloudinary _cloudinary;
    public PhotoService(CloudinarySettings cloudinarySettings)
    {
        var account = new Account(
            cloudinarySettings.CloudName,
            cloudinarySettings.ApiKey,
            cloudinarySettings.ApiSecret
        );
        _cloudinary = new Cloudinary(account);
    }
    public async Task<string> DeletePhoto(string publicId)
    {
        var deletionParams = new DeletionParams(publicId);
        var result = await _cloudinary.DestroyAsync(deletionParams);
        if(result.Error != null)
        {
            throw new Exception(result.Error.Message);
        }
        return result.Result;

    }

    public async Task<PhotoUploadResult> UploadPhoto(IFormFile file)
    {
        if (file.Length > 0)
        {
            await using var stream = file.OpenReadStream();
            var uploadParams = new CloudinaryDotNet.Actions.ImageUploadParams
            {
                File = new CloudinaryDotNet.FileDescription(file.FileName, stream),
                Transformation = new CloudinaryDotNet.Transformation().Height(500).Width(500).Crop("fill").Gravity("face"),
                Folder = "Convene"
            };
            var result = await _cloudinary.UploadAsync(uploadParams);

            if(result.Error != null)
            {
                throw new Exception(result.Error.Message);
            }
            return new PhotoUploadResult
            {
                PublicId = result.PublicId,
                Url = result.SecureUrl.AbsoluteUri
            };
        }
        return null;
    }
}