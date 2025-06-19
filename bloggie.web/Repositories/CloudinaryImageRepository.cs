//using CloudinaryDotNet;
//using CloudinaryDotNet.Actions;

//namespace Bloggie.Web.Repositories
//{
//    public class CloudinaryImageRepository : IImageRepository
//    {
//        private readonly Cloudinary cloudinary;

//        public CloudinaryImageRepository(IConfiguration configuration)
//        {
//            var account = new Account(
//                configuration["Cloudinary:CloudName"],
//                configuration["Cloudinary:ApiKey"],
//                configuration["Cloudinary:ApiSecret"]
//            );

//            cloudinary = new Cloudinary(account);
//        }

//        public async Task<string> UploadAsync(IFormFile file)
//        {
//            if (file == null || file.Length == 0)
//                return null;

//            var uploadParams = new ImageUploadParams
//            {
//                File = new FileDescription(file.FileName, file.OpenReadStream())
//            };

//            var uploadResult = await cloudinary.UploadAsync(uploadParams);

//            if (uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
//            {
//                return uploadResult.SecureUrl.ToString();
//            }

//            return null;
//        }
//    }
//}


using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System.Runtime.CompilerServices;

namespace Bloggie.Web.Repositories
{
    public class CloudinaryImageRepository : IImageRepository
    {
        private readonly IConfiguration configuration;
        private readonly Account account;

        public CloudinaryImageRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
            account = new Account(
                configuration.GetSection("Cloudinary")["CloudName"],
                configuration.GetSection("Cloudinary")["ApiKey"],
                configuration.GetSection("Cloudinary")["ApiSecret"]
                );
        }
        public async Task<string?> UploadAsync(IFormFile file)
        {
            var client = new Cloudinary(account);

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.FileName, file.OpenReadStream()),
                DisplayName = file.FileName
            };



            var uploadResult = await client.UploadAsync(uploadParams);

            if (uploadResult != null && uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return uploadResult.SecureUrl.ToString();
            }
            return null;
        }
    }
}
