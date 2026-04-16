# Persona_Bot (Python → C# → R)

A multi-turn conversational AI that gives an LLM a **custom persona via a system prompt** and maintains full conversation history across turns—built in **three languages** to understand the same concept from multiple angles: **how to define AI behavior, manage state, and build a conversation loop**.

This is **Project 2** in my compounding portfolio series. Project 1 ([Hello_LLM](https://github.com/kelvintechnical/Hello_LLM)) established the foundation: authenticate, send one message, read one response. Persona_Bot builds directly on that foundation by adding memory, personality, and a loop.

## What this project does

- **Python** (`persona_bot.py`): loads API key from `.env`, gives the AI a named persona (`Eddie`) via a `system` role message, maintains a growing `conversation_history` list, and loops until the user types `quit`, `exit`, or `bye`.
- **C#** (`csharp/persona_bot/Program.cs`): loads API key from **.NET User Secrets**, builds the same system prompt + history pattern using strongly typed SDK objects, runs an async conversation loop.
- **R** (`R/persona_bot.R`): loads API key from `.env`, maintains history as a nested list, calls the Chat Completions API via `httr2` in a `repeat` loop.

All three versions send the same system prompt:

> You are a helpful assistant named Eddie who is an expert Python mentor. You explain code clearly and encourage the user to type everything themselves.

## Why I did it (and why in three languages)

- **Transferable understanding**: The conversation loop, role system, and history pattern work the same way regardless of language — learning it in three forces me to understand the concept, not just the syntax.
- **Memory is the upgrade**: Project 1 sent one message and got one reply. Project 2 sends the full conversation every turn — that's what gives the model context. Same API, fundamentally different behavior.
- **Compounding habit**: Every project in this series reuses and extends the last. The `.env` setup, client creation, and API call from Hello_LLM are all still here — Persona_Bot just adds layers on top.

## Same workflow, three ecosystems

No matter the language, the shape of the integration is identical:

- **Persona**: define Eddie's behavior once via `system` role
- **Loop**: keep the conversation running until exit condition
- **Append**: add every user message and assistant reply to history
- **Send**: pass the full history list to the API every turn
- **Print**: extract and display the assistant's reply

Where they differ is ergonomics:

- **Python**: list of dicts; synchronous by default; fast to iterate.
- **C#**: strongly typed message objects; async-first (`await`); explicit `List<>` for history.
- **R**: nested lists for history; raw HTTP via `httr2`; `repeat` loop with `break`.

## Diesel-tech analogies (how it clicked for me)

- **System prompt = the employee handbook**  
  Before Eddie says a word, he reads the rules. Same as briefing a technician on shop procedures before they touch a vehicle.
- **Conversation history = the running work order**  
  Every fault code, every note, every technician comment stays on the job log. You send the full log every diagnostic scan — not just the latest reading.
- **`while True` loop = the engine running**  
  The loop keeps firing until you explicitly cut the ignition (`break`). It doesn't stop on its own.
- **`system` role = rail pressure baseline**  
  Sets the operating parameters before any real work begins. Change the system prompt, change the behavior — same hardware, different tuning map.
- **Appending to history = writing to the ECM log**  
  Every turn gets written to memory. Skip a turn and downstream diagnostics lose context.

## What I learned (skills gained)

- **The three OpenAI roles**: `system` (rules), `user` (input), `assistant` (response) — and why order matters.
- **Stateful conversation**: LLMs have no memory between calls — you manually pass the full history every time.
- **Loop design**: `while True` / `while(true)` / `repeat` with a clean exit condition across all three languages.
- **`.gitignore` discipline**: learned the hard way — `.gitignore` must come before `git add .`, always.
- **`git filter-branch`**: how to scrub a secret from git history when push protection blocks a commit.

## Repo structure

```text
Persona_Bot/
  persona_bot.py
  csharp/
    persona_bot/
      Program.cs
      persona_bot.csproj
  R/
    persona_bot.R
  pyproject.toml
  .gitignore
  README.md
```

## Setup

### 1) Get an API key

You'll need an `OPENAI_API_KEY`. **Never commit secrets.** This repo's `.gitignore` excludes `.env`.

### 2) Python run

Create a `.env` file in the repo root:

```bash
OPENAI_API_KEY=your_key_here
```

Using `uv`:

```bash
uv sync
.venv\Scripts\activate
uv run python persona_bot.py
```

### 3) C# run

Set the secret for the C# project:

```bash
dotnet user-secrets set "OPENAI_API_KEY" "your_key_here" --project csharp/persona_bot
```

Run:

```bash
dotnet run --project csharp/persona_bot
```

### 4) R run

Install dependencies (once):

```r
install.packages(c("dotenv", "httr2", "jsonlite"))
```

Run from repo root:

```powershell
cd D:\Kelvins_Projects\Persona_Bot
Rscript .\R\persona_bot.R
```

## Notes on security

- **Do not store real keys in code.**
- Use **User Secrets** (C#) or `.env` (Python/R) locally.
- If a key is ever committed: treat it as compromised and rotate it immediately — same as contaminated fuel in a common-rail system.

## Series

| Project | Repo | What it adds |
| --- | --- | --- |
| 01 | [Hello_LLM](https://github.com/kelvintechnical/Hello_LLM) | Auth + one API call + parse response |
| 02 | Persona_Bot ← you are here | System prompt + conversation loop + history |
| 03 | Coming soon | Agents + tool use |

## Next steps

- Add streaming output so Eddie's replies appear token by token.
- Add a `/reset` command to clear history and start a new conversation.
- Add a `max_history` limit to avoid exceeding context window on long sessions.
