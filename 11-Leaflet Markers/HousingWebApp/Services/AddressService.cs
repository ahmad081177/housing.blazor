using HousingWebApp.Models;
using System.Text.Json;

namespace HousingWebApp.Services
{
    public class AddressService
    {
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
                    if(json2.Count > 0)
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
            return null;
        }
    }
}
