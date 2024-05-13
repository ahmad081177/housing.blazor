using System;

namespace SqlGPT
{
    internal class AppUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
    internal class House
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int OwnerId { get; set; }
    }
    internal class HowTo
    {
        static async Task Main(string[] args)
        {
            //NOTE - This code should be in the blazor server project
            SQLLLM gpt = new("sk-...", SQLLLM.LLMType.OpenAI);
            gpt.AddClass(typeof(AppUser), "AppUsers", "Represents AppUsers table.");
            gpt.AddClass(typeof(House), "Houses", "Represents Houses table");

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
