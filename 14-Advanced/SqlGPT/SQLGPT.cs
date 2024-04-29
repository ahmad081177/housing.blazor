using OpenAI;
using OpenAI.Managers;
using OpenAI.ObjectModels.RequestModels;
using OpenAI.ObjectModels;
namespace SqlGPT
{
    public class SQLGPT
    {
        private readonly List<Tuple<Type, string,string>> classes = new ();
        private readonly string? apikey;

        public SQLGPT(string apikey="")
        {
            if(!string.IsNullOrEmpty(apikey))
            {
                this.apikey = apikey;
            }
            else
            {
                this.apikey = Configuration.ReadAPIKey();
            }
        }
        public void AddClass(Type ctype, string tablename, string desc="")
        {
            classes.Add(new (ctype, tablename, desc));
        }

        public async Task<string> QueryAsync(string query, bool isSql=true)
        {
            if(string.IsNullOrEmpty(this.apikey))
            {
                throw new Exception("API Key is not set");
            }
            var client = new OpenAIService(new OpenAiOptions() { ApiKey = this.apikey });
            
            string queryPrompt = GetPrompt(query);
            
            var completionResult = await client.ChatCompletion.CreateCompletion(new ChatCompletionCreateRequest
            {
                Messages = new List<ChatMessage>
                {
                    ChatMessage.FromSystem(GetSystemPrompt()),
                    ChatMessage.FromUser(queryPrompt)
                },
                Model = Models.Gpt_3_5_Turbo,
                MaxTokens = 250//optional
            });
            if (completionResult.Successful)
            {
                return completionResult.Choices.First().Message.Content;
            }
            throw new Exception(completionResult.Error?.Messages[0]);
        }
        private static string GetSystemPrompt()
        {
            return "You are a helpful assistant expert in SQL queries, helping user to generate valid SQL query out of his questions.";
        }
        private string GetPrompt(string userquery)
        {
            string prompt = GetPrompt();
            string ret = prompt.Replace("<query>", userquery);
            Console.WriteLine("Prompt:\n" + ret);
            return ret;
        }
        private string GetPrompt()
        {
            string prompt = $@"
You are an expert at writing SQL queries throught a given Natural Language description of the OBJECTIVE. 

Given MSSQL localDB that was created by Entity Framework\n";
            
            prompt += $@"\n###SQL Table Names:###\n";
            foreach (var c in classes)
            {
                prompt += $"{c.Item2}\n ";
            }
            prompt += $@"\n";

            prompt += $@"\n\n###CSharp Class - To be used to extract Attribute Names:###\n";
            foreach (var c in classes)
            {
                string cs = SqlGPTUtils.GenerateClassString(c.Item1);
                prompt += $"{cs}\n";
            }
            //prompt += "Here is the description of the classes:\n\n";
            //for(int i=0; i<classDesc.Count; i++)
            //{
            //    prompt += $"{classes[i]} : {classDesc[i]}\n";
            //}
            
            prompt += $@"\n
You should generate valid SQL SELECT query based on the above SQL Tables info.
Your output should be ONLY SQL SELECT query without any explaination or any other strings.
se Transact-SQL syntax to write the query compatible with Microsoft SQL Server.

###Question####
<query>

###Answer###\n
";
            return prompt;
        }
    }
}
