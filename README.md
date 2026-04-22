# Persona_Bot (Python + C# + R)

A multi-turn conversational AI that gives an LLM a **custom persona via a system prompt**, keeps **full conversation history** across turns, and runs until you exit—implemented in **three languages** so the same ideas land in Python, C#, and R: **roles, stateful history, and a conversation loop**.

This is **Project 2** in my compounding portfolio series. [Hello_LLM](https://github.com/kelvintechnical/Hello_LLM) was Project 1: one prompt, one reply, same stack in Python, C#, and R—**authenticate, build a request, call the API, parse JSON**. Persona_Bot is the natural next step: everything from Hello_LLM still applies; this repo **reuses those habits** and adds **persona + loop + memory**.

## From Hello_LLM to Persona_Bot (how the skills combine)

Hello_LLM taught the **request lifecycle** end to end. Persona_Bot does not replace that—it **extends** it:

| Skill from [Hello_LLM](https://github.com/kelvintechnical/Hello_LLM) | How Persona_Bot uses it |
| --- | --- |
| **Secrets** — `.env` (Python/R), User Secrets (C#) | Same patterns: Python and R load `OPENAI_API_KEY` from `.env`; C# reads from `dotnet user-secrets`. |
| **HTTP / SDK choice** — Python SDK, C# OpenAI client, R `httr2` + JSON | Same split: Python uses the OpenAI client; C# uses `ChatClient` / `CompleteChatAsync`; R builds the Chat Completions body and calls the REST endpoint explicitly. |
| **Messages as structured input** | Hello_LLM used a minimal `messages` list. Here the list **grows every turn**: `system` once, then alternating `user` / `assistant`. |
| **Parse the assistant text from the response** | Same extraction idea; the payload is just deeper because the conversation is longer. |

What Project 2 adds on top: **`system` role as persona**, **append-after-each-turn**, and **send full history every call** (because the API is stateless—there is no server-side chat memory unless you send it).

## What this project does

- **Python** (`persona_bot.py`): loads the API key from `.env`, sets persona **Eddie** via a `system` message, appends user/assistant turns to `conversation_history`, calls `gpt-4o-mini` until you type `quit`, `exit`, or `bye`.
- **C#** (`csharp/csharp_persona_bot/Program.cs`): loads the key from **.NET User Secrets**, uses typed `ChatMessage` history, `await`s `CompleteChatAsync` in a `while` loop—same exit words as Python.
- **R** (`R/persona_bot.R`): loads `.env`, keeps history as nested lists, uses `httr2` + `jsonlite` against the Chat Completions endpoint, `repeat` / `break` for the loop—same behavior, explicit HTTP.

All three use the same **system** persona (Eddie, Python mentor—encouraging, procedural, not doing the typing for you).

## Why I did it (and why in three languages)

- **Transferable understanding**: Loop, roles, and history behave the same in every language; three implementations stress the **concept**, not one syntax.
- **Memory is the upgrade**: Project 1 = one shot. Project 2 = **full transcript every request**—that is what “memory” means with a stateless API.
- **Compounding habit**: Same repo hygiene as Hello_LLM (`.gitignore`, no keys in source); same three-ecosystem layout so I can compare ergonomics honestly.

## Same workflow, three ecosystems

Shape of the integration (same as Hello_LLM, with extra steps):

- **Supply**: load `OPENAI_API_KEY` securely (`.env` or User Secrets).
- **Persona**: one `system` message up front.
- **Loop**: read input until exit phrase.
- **Append**: push each `user` message, then each `assistant` reply onto history.
- **Delivery**: send `model` + full `messages` every turn.
- **Readback**: print the latest assistant content.

Where they differ is mostly ergonomics:

- **Python**: list of dicts; SDK hides HTTP; quick iteration.
- **C#**: `List<ChatMessage>`, async-first, model bound on the client.
- **R**: nested lists, `httr2` shows the wire format—good for “what actually gets sent.”

## Diesel-tech analogies (how it clicked for me)

- **System prompt = the employee handbook** — rules before the first customer interaction.
- **Conversation history = the running work order** — every note stays on the job; you resend the whole log each round.
- **The loop = the engine running** — runs until you cut ignition (`break` / exit phrase).
- **Appending history = ECM log entries** — skip a turn and downstream context is wrong.

## What I learned (skills gained)

- **Roles**: `system` / `user` / `assistant` and why order matters.
- **Stateful conversation in a stateless API**: you are the memory layer.
- **Loop design**: `while True`, `while (true)`, `repeat` + `break`, consistent exit handling.
- **Carrying Hello_LLM forward**: secrets, clients vs raw HTTP, and JSON-shaped `messages`—then layering persona and history on top.

## Repo structure

```text
Persona_Bot/
  persona_bot.py
  csharp/
    csharp_persona_bot/
      Program.cs
      csharp_persona_bot.csproj
  R/
    persona_bot.R
  pyproject.toml
  uv.lock
  .gitignore
  README.md
```

## Setup

### 1) Get an API key

You need an `OPENAI_API_KEY`. **Never commit secrets.** This repo’s `.gitignore` excludes `.env`.

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
dotnet user-secrets set "OPENAI_API_KEY" "your_key_here" --project csharp/csharp_persona_bot
```

Run:

```bash
dotnet run --project csharp/csharp_persona_bot
```

### 4) R run

Install dependencies once (in R or via `Rscript`):

```r
install.packages(c("dotenv", "httr2", "jsonlite"))
```

Run from the **repo root** so `.env` is found:

```powershell
cd D:\Kelvins_Projects\Persona_Bot
Rscript .\R\persona_bot.R
```

Tip: if `Rscript` is not on your `PATH`, call it with the full path to `Rscript.exe` (same idea as [Hello_LLM’s README](https://github.com/kelvintechnical/Hello_LLM)).

## Notes on security

- **Do not store real keys in code.**
- Use **User Secrets** (C#) or **`.env`** (Python/R) locally only.
- If a key is ever committed, treat it as compromised and **rotate** it immediately.

## Series

| Project | Repo | What it adds |
| --- | --- | --- |
| 01 | [Hello_LLM](https://github.com/kelvintechnical/Hello_LLM) | Auth + one API call + parse response |
| 02 | Persona_Bot ← you are here | System persona + conversation loop + full history |
| 03 | Coming soon | Agents + tool use |

## Next steps

- Streaming replies token by token.
- A `/reset` command to clear history.
- Optional `max_history` to stay inside context limits on long sessions.
