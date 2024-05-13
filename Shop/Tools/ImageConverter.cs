namespace Shop.Tools
{
    public static class ImageConverter
    {
        public static byte[] ImgConverter(IFormFile file)
        {
            using (MemoryStream memoryStream1 = new MemoryStream())
            {
                file.CopyTo(memoryStream1);
                return memoryStream1.ToArray();
            }
        }

        public static async Task<string> SaveIcone(IFormFile file, string categorie, IWebHostEnvironment hostingEnvironment)
        {
            Guid newName = Guid.NewGuid();
            string fileName = newName.ToString() + file.FileName;

            string uploadsFolder = "Assets/" + categorie;
            string relativeUploadsPath = Path.Combine(uploadsFolder, fileName);

            string absoluteUploadsPath = Path.Combine(hostingEnvironment.ContentRootPath, relativeUploadsPath);

            if (!Directory.Exists(Path.GetDirectoryName(absoluteUploadsPath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(absoluteUploadsPath));
            }

            using (FileStream fs = new FileStream(absoluteUploadsPath, FileMode.Create))
            {
                await file.CopyToAsync(fs);
            }

            // Obtention du chemin relatif à partir du chemin absolu
            return Path.Combine("", relativeUploadsPath).Replace('\\', '/'); // Chemin relatif avec des slashs
        }
    }
}
