using CloudinaryDotNet.Actions;

namespace webApi.Interfaces
{
    public interface IPhotoService
    {
        Task<ImageUploadResult> UploadPhotoAsync(IFormFile photo);

        Task<DeletionResult> DeletePhotoAsync(string publidId);

    }
}
