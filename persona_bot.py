import os
# C# Equivalent: using System;
# R Equivalent: no import needed

from openai import OpenAI
# C#L using OpenAI;
# R Equivalent: library(openai)

from dotenv import load_dotenv

load_dotenv(override=True)
# C# // automatically via builder.Configuration in Program.cs
# R: dotenv::load_dot_env()

api_key = os.getenv("OPENAI_API_KEY")
# c# EQUIVALENT:  var apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY")
# R equivalent: api_key <- Sys.getenv("OPENAI_API_KEY")


client = OpenAI(api_key=api_key)
#C = var client = new OpenAIClient(apiKey) which is a constructor
#R = client <- OpenAI(api_key = api_key)

conversation_history = []
# C# = var conversationHistory = new List<object>();
# R = conversation_history <- list()

system_prompt = (
    "You are a helpful assistant named Eddie who is an expert Python mentor. "
    "You explain code clearly and encourage the user to type everything themselves. "
    "Be kind, provide hints on what direction to go, explain procedurally, "
    "and allow them the space to think for themselves."
)
# C# = var systemPrompt = "..";
# R = system_prompt <- ".."
conversation_history.append({"role": "system", "content": system_prompt})

# In C# it would be something like: conversationHistory.add(new {role = "user", content = systemPrompt});
# R it would be conversation_history <- c(conversation_history, list(role = "user", content = system_prompt))

while True:
    #C While (true) {
    #}
    #R repeat {}
    user_input = input("You: ")


# C# string userInput = Console.ReadLine();
# R user_input <- readline("You: ")

    if user_input.lower() in ["exit", "quit", "bye"]:
        print("Eddie: signing off ...")

    # C#: if (userInput.ToLower() == "quit")
    #  R: if (tolower(user_input) == "quit")\
        break
    conversation_history.append({"role": "user", "content": user_input})


#What: Sends the entire conversation history to GPT and gets Eddie's response back.
# Why: Instead of sending one message like Project 1, 
# you're now sending the full history list — that's
#  what gives Eddie memory.
    response = client.chat.completions.create(
        model="gpt-4o-mini",
        messages=conversation_history,
)

# var response = await Client.Chat.Completions.CreateAsync(
#     new ChatCompletionCreateRequest{
#         Model = "gpt-4o-mini",
#         Message = conversation_history
#     });

#R response <- POST(url, body=list(model="gpt-4o-mini", messages=conversation_history))

# Eddie's response:
    print("Eddie: ", response.choices[0].message.content)