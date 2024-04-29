namespace HousingWebApp.Services
{
    public class ImageService
    {
        public string ImagesDir { get; private set; }
        public ImageService(string imagesDir)
        {
            ImagesDir = imagesDir;
        }
        //This method is called when user creates new house
        public void StoreImage(MemoryStream ms, int houseid, string filename)
        {
            //store image ms to disk
            string imagePath = Path.Combine(ImagesDir,houseid.ToString(), filename);
            using (FileStream fs = new
                FileStream(imagePath, FileMode.Create, FileAccess.Write))
            {
                ms.WriteTo(fs);
            }
        }
        //Load image from disk and convert to base64 to display in html
        //<img src='@ImageToBase64("fname")'/>
        public string ImageToBase64(string imagePath)
        {
            try
            {
                byte[] imageBytes = File.ReadAllBytes(imagePath);
                string base64String= Convert.ToBase64String(imageBytes);
                string imageType = Path.GetExtension(imagePath).TrimStart('.').ToLower();

                string dataUrl = $"data:image/{imageType};base64,{base64String}";
                return dataUrl;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error converting image to base64: {ex.Message}");
                return "";
            }
        }
    }
}
