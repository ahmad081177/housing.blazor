namespace SqlGPT
{
    public class SQLLLM
    {
        private readonly List<Tuple<Type, string,string>> classes = new ();
        private readonly string apikey;
        private readonly LLMType llmtype;

        public enum LLMType
        {
            OpenAI,
            Gemini
        }
        public SQLLLM(string apikey, LLMType llmtype = LLMType.OpenAI)
        {
            if(string.IsNullOrEmpty(apikey))
            {
                throw new Exception("API Key is empty");
            }
            this.apikey = apikey;
            this.llmtype = llmtype;
        }
        public void AddClass(Type ctype, string tablename, string desc="")
        {
            classes.Add(new (ctype, tablename, desc));
        }

        public async Task<string> QueryAsync(string query, bool isSql = true)
        {
            ILLM llm = llmtype switch
            {
                LLMType.OpenAI => new OpenAIClient(),
                LLMType.Gemini => new GeminiClient(),
                _ => throw new Exception("Invalid LLM Type")
            };
            llm.Init(this.apikey);
            return await llm.RunQueryAsync(GetPrompt(query));
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
You are an expert at writing SQL queries from user question. 

Given information about the MSSQL localDB created by Entity Framework\n";
            
            prompt += $@"\n## SQL Table Names ##\n";
            foreach (var c in classes)
            {
                prompt += $"- {c.Item2}\n ";
            }
            prompt += $@"\n";

            prompt += $@"\n\n## CSharp Classes (For Attribute Names Extraction) ##\n";
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
## Important Notes: ##
1- Generate a valid SQL SELECT query based on the user query and the provided SQL Tables info.
2- Your answer should contain ONLY a SELECT query without any other SQL commands (INSERT, UPDATE, DELETE) or additional strings.
3- Ensure that the SELECT query is safe from SQL Injection.
4- Strictly adhere to generating SELECT queries; do not include any other SQL commands.
5- Ensure that the SELECT query does not retrieve sensitive information such as passwords.


## User Question ##
<query>

## Your SQL Answer ##\n
";
            return prompt;
        }
    }
}
