using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace IKEA.BLL.Common.Services.Attachments
{
    public class AttachmentServices : IAttachmentServices
    {
        private readonly List<string> AllowedExtensions = new List<string>() { ".jpg",".jpeg",".png",};
        private const int FileMaxMinSize = 2_097_152; 
        public string UploadImage(IFormFile file, string FolderName)
        {
           var fileExtention=Path.GetExtension(file.FileName);

            if(!AllowedExtensions.Contains(fileExtention))
            
                throw new Exception("File type is not supporte");
            if (file.Length > FileMaxMinSize)
                throw new Exception("File size is too large");
            var FolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", FolderName);
            if (!Directory.Exists(FolderPath))
                Directory.CreateDirectory(FolderPath);
            var fileName = $"{Guid.NewGuid()}_{ file.FileName} ";

            var filePath = Path.Combine(FolderPath, fileName);

            using var fs = new FileStream(filePath, FileMode.Create);
            file.CopyTo(fs);
            return fileName;

        }
        public bool DeleteImage(string filePath)
        {
            if(File.Exists(filePath))
            {
                File.Delete(filePath);
                return true;
            }
            return false;
        }

       
    }
}
