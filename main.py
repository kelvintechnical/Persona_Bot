import os  #in C# this would be using System; 
from openai import OpenAI  
from dotenv import load_dotenv

load_dotenv(override=True)
#in C# this would be using OpenAI
#in R, this would be: library(httr2)
import os  #in C# this would be using System; 
#in R, this wold be: Sys.getenv()

api_key = os.getenv("OPENAI_API_KEY")  #in C# this would be: var apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY")
#in R, this would be api_key <- sys.getenv("OPENAI_API_KEY"))
client = OpenAI(api_key=api_key) # C#  EQUIVALENT is var client = newOpenAIClient(apiKey)
# In R this would be: client setup usually starts by storing the key and using it in the request


#Add my first LLM call:
response = client.chat.completions.create( #In C# this would be var response = await client.Chat.Completions.CreateChatCompletionAsync(
#in R, this would be response <- POST(...) using httr2
    model="gpt-4o-mini", #In C# this would still be model: "gpt-4o-mini" 
    #in R, this would be: include model in JSON body
    messages= [
        # In C#, this would be: messages new[] {...}
        #in R, tis would be: list(messages = list(...))
        {
            "role": "user", "content": "Hello, how are you today?"
            # In C#, this would be: new ChatMessages("user", ...)
            #in R, this would be list(role="user", content="...")
        }
    ]   
)

print(response.choices[0].message.content)

#C
# Console.WriteLine(response.Choices[0].Messages.Content)
#R
# print(response$choices[[1]]$messages$content)   R DOES NOT USE 0 INDEXING 
