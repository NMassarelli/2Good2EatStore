using _2Good2EatStore.Data.Interfaces;
using Microsoft.AspNetCore.Components.Forms;

namespace _2Good2EatStore.Data
{
    public class FileUploadUtilityService(IWebHostEnvironment webHostEnvironment) : IFileUploadUtilityService
    {

        private readonly IWebHostEnvironment _webHostEnvironment = webHostEnvironment;

        public async Task<string> MoveFileToRoot(IBrowserFile file)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(file.Name);
                var fileName = file.Name + fileInfo.Extension;
                var folderDirectory = $"{_webHostEnvironment.WebRootPath}\\Images";
                var path = Path.Combine(_webHostEnvironment.WebRootPath, "Images", fileName);

                var memoryStream = new MemoryStream();
                await file.OpenReadStream(5000000).CopyToAsync(memoryStream);

                if (!Directory.Exists(folderDirectory))
                {
                    Directory.CreateDirectory(folderDirectory);
                }

                await using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write))
                {
                    memoryStream.WriteTo(fs);
                }

      
                return path;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

    }
}
