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

 var config = new ConfigurationBuilder()
 .AddUserSecrets<Program>()
 .Build();

// Python equivalent   # this is what load_dotenv() does internally
// R equivalent: Sys.getenv() reads directly, no builder needed

var apiKey = config["OPENAI_API_KEY"];

// What: Reads your API key out of the configuration object you just built.
// Why: config["KEY"] is how you access any 
//value loaded by ConfigurationBuilder — like indexing a dictionary.

// Python equivalent: api_key = os.getenv("OPENAI_API_KEY")
// R equivalent: api_key <- Sys.getenv("OPENAI_API_KEY")

var client = new ChatClient("gpt-4o-mini", apiKey);

// What: Creates the OpenAI chat client using your model name and API key.
// Why: This is the object you'll use to send messages — same job as client = OpenAI(api_key=api_key) in Python.
// Python equivalent: client = OpenAI(api_key=api_key)
// R equivalent: # no single client object — key passed directly in each request