using BlogDotNet.EntityLayer.DTOs.Images;
using BlogDotNet.EntityLayer.Entities.Enums;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogDotNet.ServiceLayer.Helpers.Images
{
    public class ImageHelper : IImageHelper
    {
        private readonly string wwwroot;
        private readonly IWebHostEnvironment env;
        private const string imageFolder = "image";
        private const string articleImagesFolder = "article-images";
        private const string userImageFolder = "user-images";

        public ImageHelper(IWebHostEnvironment env)
        {
            this.env = env;
            wwwroot = env.WebRootPath;
        }

        private string ReplaceInvalidChars(string fileName)
        {
            foreach (char c in System.IO.Path.GetInvalidFileNameChars())
            {
                fileName = fileName.Replace(c, '_');

            }
            return fileName;
        }


        public async Task<ImageUploadedDto> Upload(string name, IFormFile imageFile, ImageType imageType, string folderName = null)
        {
            folderName ??= imageType==ImageType.User?userImageFolder: articleImagesFolder;

            if(!Directory.Exists($"{wwwroot}/{imageFolder}/{folderName}")) 
                Directory.CreateDirectory($"{wwwroot}/{imageFolder}/{folderName}");

            string oldFileName=Path.GetFileNameWithoutExtension(imageFile.FileName);
            string fileExtension = Path.GetExtension(imageFile.FileName);
            name =ReplaceInvalidChars(name);

            DateTime dateTime = DateTime.Now;

            string newFileName=$"{name}_{dateTime.Millisecond}{fileExtension}";
            var path=Path.Combine($"{wwwroot}/{imageFolder}/{folderName}",newFileName);

            await using var stream =new FileStream(path,FileMode.Create,FileAccess.Write,FileShare.None,1024*1024,false);
            await imageFile.CopyToAsync(stream);
            await stream.FlushAsync();

            string message = imageType == ImageType.User
                ? $"{newFileName} isimli kullanıcı resmi başarıyla eklenmiştir"
                : $"{newFileName} isimli makale resmi başarıyla eklenmiştir";

            return new ImageUploadedDto()
            {
                FullName = $"{folderName}/{newFileName}",
            };

        }



        public void Delete(string imageName)
        {
            var fileToDelete = Path.Combine($"{wwwroot}/{imageFolder}/{imageName}");
            if (File.Exists(fileToDelete))
                File.Delete(fileToDelete);

        } 
    }
}
