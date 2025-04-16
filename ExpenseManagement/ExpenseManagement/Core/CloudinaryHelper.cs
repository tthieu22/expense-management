using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

public class CloudinaryHelper
{
    private static readonly string cloudName = "dyjo6bsks";
    private static readonly string apiKey = "957135176966714";
    private static readonly string apiSecret = "ThO-6wc03uwhTTxVi6rEM0ohedY";
    private Cloudinary cloudinary;

    public CloudinaryHelper()
    {
        Account account = new Account(cloudName, apiKey, apiSecret);
        cloudinary = new Cloudinary(account);
    }

    public async Task<string> UploadImageAsync(string filePath)
    {
        if (string.IsNullOrEmpty(filePath))
            return null;

        var uploadParams = new ImageUploadParams()
        {
            File = new FileDescription(filePath),
            Folder = "expense_images"
        };

        var uploadResult = await Task.Run(() => cloudinary.Upload(uploadParams));

        return uploadResult.StatusCode == System.Net.HttpStatusCode.OK ? uploadResult.SecureUrl.AbsoluteUri : null;
    }

    public async Task<string> UploadImageAsync(Bitmap image)
    {
        if (image == null) return null;

        using (MemoryStream ms = new MemoryStream())
        {
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            ms.Seek(0, SeekOrigin.Begin);

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription("image.jpg", ms),
                Folder = "expense_images"
            };

            var uploadResult = await Task.Run(() => cloudinary.Upload(uploadParams));

            return uploadResult.StatusCode == System.Net.HttpStatusCode.OK ? uploadResult.SecureUrl.AbsoluteUri : null;
        }
    }

    // Update width strig 
    public async Task<string> UpdateImageAsync(string publicId, string newFilePath)
    {
        if (string.IsNullOrEmpty(publicId) || string.IsNullOrEmpty(newFilePath))
            return null;

        var uploadParams = new ImageUploadParams()
        {
            File = new FileDescription(newFilePath),
            PublicId = publicId, 
            Folder = "expense_images"
        };

        var uploadResult = await Task.Run(() => cloudinary.Upload(uploadParams));

        return uploadResult.StatusCode == System.Net.HttpStatusCode.OK ? uploadResult.SecureUrl.AbsoluteUri : null;
    }

    // Update with bitmap take a pictrue
    public async Task<string> UpdateImageAsync(string publicId, Bitmap image)
    {
        if (string.IsNullOrEmpty(publicId) || image == null)
            return null;

        using (MemoryStream ms = new MemoryStream())
        {
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            ms.Seek(0, SeekOrigin.Begin);

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription("image.jpg", ms),
                PublicId = publicId,
                Folder = "expense_images"
            };

            var uploadResult = await Task.Run(() => cloudinary.Upload(uploadParams));

            return uploadResult.StatusCode == System.Net.HttpStatusCode.OK ? uploadResult.SecureUrl.AbsoluteUri : null;
        }
    }

    public async Task<bool> DeleteImageAsync(string publicId)
    {
        if (string.IsNullOrEmpty(publicId))
            return false;

        var deleteParams = new DeletionParams(publicId);
        var deleteResult = await Task.Run(() => cloudinary.Destroy(deleteParams));

        return deleteResult.StatusCode == System.Net.HttpStatusCode.OK;
    }
    public string GetPublicIdFromUrl(string url)
    {
        Uri uri = new Uri(url);
        string path = uri.AbsolutePath;
        string[] segments = path.Split('/');
        int versionIndex = Array.IndexOf(segments, "v" + segments[1].Substring(1));
        string publicId = string.Join("/", segments.Skip(versionIndex + 1));
        publicId = Path.GetFileNameWithoutExtension(publicId);
        return publicId;
    }

}
