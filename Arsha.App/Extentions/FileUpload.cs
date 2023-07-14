using Arsha.Core.Entities;

namespace Arsha.App.Extentions
{
    public static class FileUpload
    {
        public static string createimage(this IFormFile FormFile,string root,string path)
        {
            
            
            string FileName = Guid.NewGuid().ToString() + FormFile.FileName;
            string FullPath = Path.Combine(root, path, FileName);

            using (FileStream fileStream = new FileStream(FullPath, FileMode.Create))
            {
                FormFile.CopyTo(fileStream);
            }
            return FileName;
        }
    }
}
