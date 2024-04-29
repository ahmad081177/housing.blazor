using System;
using HousingModels.Models;

namespace SqlGPT
{
    internal class HowTo
    {
        static async Task Main(string[] args)
        {
            //NOTE - This code should be in the blazor server project
            SQLGPT gpt = new("sk-...");
            gpt.AddClass(typeof(AppUser), "AppUsers", "Represents AppUsers table.");
            gpt.AddClass(typeof(House), "Houses", "Represents Houses table");
            gpt.AddClass(typeof(Address), "Addresses", "Represents Addresses table - for house and appuser");
            gpt.AddClass(typeof(HouseDetails), "HousesDetails", "Represents HousesDetails table - more details of the house");
            gpt.AddClass(typeof(HouseImages), "HouseImages", "Represents HouseImages table - images of the house");
            gpt.AddClass(typeof(Review), "Reviews", "Represents Reviews table - each review is from user on one house");

            Console.WriteLine("Enter your query: ");
            string? query = Console.ReadLine();
            while (!string.IsNullOrEmpty(query) && query != "exit")
            {
                // QueryAsync returns a string (JSON)
                string response = await gpt.QueryAsync(query);
                Console.WriteLine(response);

                Console.WriteLine("Enter your query: ");
                query = Console.ReadLine();
            }

            //string query = "Get all houses that I am owning in Israel. My email: ahmad@gmail.com";
            //string response = await gpt.QueryAsync(query);
            //Console.WriteLine(response);

            Console.ReadKey();
        }
    }
}
