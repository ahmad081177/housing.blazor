using HousingWebApp.Models;
using Newtonsoft.Json.Linq;
using System.Text.Json;

namespace HousingWebApp.Services
{
    public class AddressService
    {
        private IConfiguration configuration;

        public AddressService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string? GetGKey()
        {
            var section = configuration.GetSection("AppSettings");
            if(section != null)
            {
                return section["Google:GKey"];
            }
            return null;
        }
        public async Task<Tuple<double, double>> ConvertGAddress(Address address)
        {
            //if gkey==null throw exception
            string gkey = GetGKey() ?? throw new Exception("Google Maps API key not found in configuration");
            
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", "C# Housing app");
            string saddress = address.Country + "," + address.City + "," + address.Street;
            string url = $"https://maps.google.com/maps/api/geocode/json?address={saddress}&key={gkey}";
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                JObject jsonResponse = JObject.Parse(responseBody);
                if (jsonResponse != null)
                {
                    // Check if the API response indicates success
                    string status = jsonResponse["status"].ToString();
                    if (status != "OK")
                    {
                        throw new Exception($"Google Maps API request failed with status: {status}");
                    }

                    // Extract latitude and longitude from the first result
                    double latitude = (double)jsonResponse["results"][0]["geometry"]["location"]["lat"];
                    double longitude = (double)jsonResponse["results"][0]["geometry"]["location"]["lng"];

                    return new Tuple<double, double>(latitude, longitude);
                }
            }

            return new Tuple<double, double>(0, 0);
        }
        public async Task<Tuple<double,double>> ConvertAddress(Address address)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("User-Agent", "C# Housing app");
                string saddress = address.Country + "," + address.City + "," + address.Street;
                string url = $"https://nominatim.openstreetmap.org/search?addressdetails=1&q={saddress}&format=jsonv2&limit=1";
                HttpResponseMessage response = await client.GetAsync(url);
                if(response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    var json2 = JsonSerializer.Deserialize<List<Dictionary<string, object>>>(json);
                    if(json2?.Count > 0)
                    {
                        double lat = double.Parse(json2[0]["lat"].ToString());
                        double lon = double.Parse(json2[0]["lon"].ToString());
                        return new Tuple<double, double>(lat, lon);
                    }
                }
            }catch(Exception e)
            {
             //   throw new Exception(e.Message);
            }
            return new Tuple<double, double>(0,0);
        }
    }
}
