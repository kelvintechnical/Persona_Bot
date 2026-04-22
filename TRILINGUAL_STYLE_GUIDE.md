# Trilingual Comment Style Guide
## Purpose
Every file in this project (C#, Python, R) is commented to show the same
concept across all three languages simultaneously. This is a learning tool —
reading one file teaches you all three.

## Comment Format (C# example)
```csharp
var history = new List();
// Python: history = []
// R: history <- list()
// WHAT: Creates an empty typed list to hold all conversation messages
// WHY: OpenAI API is stateless — full history must be sent every turn for memory
// SYNTAX: List — generic type constraint; only ChatMessage objects allowed
```

## Rules
1. Every line gets comments — imports, braces, declarations, everything
2. Equivalents must be real runnable code, not descriptions
3. If no equivalent exists: `# No direct equivalent — [reason]`
4. WHAT = what the line does (1 sentence)
5. WHY = the intent/benefit (1 sentence)  
6. SYNTAX = only when a token would confuse a learner crossing languages

## Quick Cursor Prompt (paste into any Cursor chat)
> "Follow the trilingual comment style in TRILINGUAL_STYLE_GUIDE.md.
> For every line, add: Python equivalent, R equivalent, WHAT, WHY,
> and SYNTAX note if needed. Keep equivalents as real runnable code."

Next steps:

Commit both files: git add . && git commit -m "docs: add trilingual style guide"
In any future Cursor chat just say: "follow TRILINGUAL_STYLE_GUIDE.md"
Works for Hello_LLM, Persona_Bot, and every future polyglot project
