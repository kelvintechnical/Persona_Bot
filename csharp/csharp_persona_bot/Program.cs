using System;
//Python equivalent to import os
//R equivalent: built-in

using OpenAI.Chat;
//Python equivalent: from openai import openai
//R equivalent: library(openai)

using Microsoft.Extensions.Configuration;
 //Python equivalent: from dotenv import load_dotenv
 //R equivalent: library(dotenv)

//Starts building a configuration object that will load your secrets
//Why: C# uses a "builder pattern" to construct configuration 
//You chain methods into it to tell it where to look for secrets

 var config = new ConfigurationBuilder().AddUserSecrets<Program>().Build();
// Python equivalent   # this is what load_dotenv() does internally
// R dotenv::load_dotenv()

var apiKey = config["OPENAI_API_KEY"] ?? throw new InvalidOperationException("OPENAI_API_KEY is not set");
//Python: api_key = os.getenv("OPENAI_API_KEY")
//R: dotenv::load_dotenv()

var client = new ChatClient("gpt-4o-mini", apiKey);
//Python: client = OpenAI(api_key=api_key) #model passed later in .create()
//R: # no client object -- api_key is passed as Bearer token header in each request
//WHAT: Creates the reuseable OpenAI client object bound to gpt-4o-mini and your key
//WHY: One client instance resuded across the loop -- no re-auth overhead per turn
//NOTE: In the .NET SDK the model is set at client creation; Python sets it per call

var history = new List<ChatMessage>();
// Python: history = []
// R: history <- list()
// WHAT: Initializes/creates an empty list to store conversation history
// WHY: OpenAI API is stateless -- you must sned the full history every turn
// for the model to have memory
// SYNTAX: List<ChatMessages> -- the <ChatMessage> is generic constraint
// which is  a class that represents a single message in the conversation and what types can be added

var systemPrompt = 
// Python: system_prompt = ()
// R: system_prompt <- ()
// WHAT: Declares the personal instruction string that controls Eddie's behavior all session 
// WHY: Defining it as a variable keeps it readable and separate from the history.Add call

    "You are a helpful assistant named Eddie who is an expert Python Mentor." +
    "Explain the code clearly and encourage the user to type everything themselves.  " +
    "Be kind, provide hints on what direction to go, explain procedurally, and allow them the space to think for themselves."
;


history.Add(ChatMessage.CreateSystemMessage(systemPrompt));
// Python: history.append({"role": "system", "content": system_prompt})
// R: conversation_history <- c(conversation_history, list(role = "system", content = system_prompt))

while(true)
// Python: while True:
// R: repeat {}
// WHAT: Starts an infinite loop -- runs unil a break is hit
// WHY: Chat loops are naturally infinite; the exit condition is user input, not a counter(integer)
// SYNTAX: true is lowercare in C#, True is capialized in Python and R uses repeat with no condition
{
    Console.Write("You: ");
    // Python: # prompt is the argument to input() below -- no separate print needed
    // R: # prompt is the argument to readline() below -- no separate cat() needed
    // WHAT: Prints the "You: " prompt without a newline so input appears on the same line
    // WHY: Console.Write() vs WriteLine - White stays on the lame line:

    var input = Console.ReadLine() ?? "";
    // Python: user_input = input("You: ")
    // R: user_input <- readline("You: ")
    // WHAT: Reads a full line of user input from the terminal
    // WHY: ?? ""guards against null -- readline() returns null is stdin closes unexpectedly

    if (input.ToLower() is "quit" or "exit" or "bye")
    // Python: if user_input.lower() in ["exit", "quit", "bye"]:
    // R: if (tolower(user_input)  %in% c("exit", "quit", "bye")):
    // WHAT: checks if user typed a quit command, case-insensitively
    // WHY: Normalize to lowercase first so "Quit" and "QUIT" both work
    {
        Console.WriteLine("Eddie: signing off.........");
        //Python print("Eddie: signing off..........")
        //R cat("Eddie: signing off .........")
        break;
        // Python: break
        // R: break
        // WHAT: Exists the loop entirely

    }

    history.Add(ChatMessage.CreateUserMessage(input));
    // Python: history.append({"role": "user", "content": user_input})
    // R: conversation_history <- c(conversation_history, list(role = "user", content = user_input))
    //WHAT: Appends the user's message to history before sending to the API
    // WHY: OpenAI API's is stateless 
    
    ChatCompletion response = await client.CompleteChatAsync(history);
    // Python: response = client.chat.completions.create(
    // model="gpt-4o-mini",
    // messages=history
    // )

    //R: response <- request("https://api.openai.com/v1/chat/completions") |>
    // req_headers(Authorization=paste("Bearer", api_key), 'Content-Type'="application/json") |>
    // req_body_json(list(model="gpt-4o-mini", message=history)) |>
    // req_perform() |> resp_body_json()

// WHAT: Sends the full conversation history to OpenAI and awaits the reply
// WHY: Full history = model memory; await = non-blocking async network call
// SYNTAX: await = async/await pattern in C#; async in Python; async in R
var reply = response.Content[0].Text;

history.Add(ChatMessage.CreateAssistantMessage(reply));

Console.WriteLine($"\nEddie: {reply}\n");
}