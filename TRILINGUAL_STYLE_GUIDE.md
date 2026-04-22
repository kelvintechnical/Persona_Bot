# Trilingual Comment Style Guide (C# ⇄ Python ⇄ R)

## Goal
Every **C#** line is annotated with the closest runnable equivalent in **Python** and **R**, plus a short explanation of intent.

## Required block (per line)
- `// Python: <runnable code>`
- `// R: <runnable code>`
- `// WHAT: <1 sentence>`
- `// WHY: <1 sentence>`
- `// SYNTAX: <only if cross-language confusing>`

## Example

```csharp
var history = new List<ChatMessage>();
// Python: history = []
// R: history <- list()
// WHAT: Creates an empty message list for the chat history.
// WHY: The API is stateless, so prior messages must be resent each turn.
// SYNTAX: List<T> is a generic typed list; T is ChatMessage here.
```

## Rules
- **Every line**: includes imports, braces, declarations, and “boring” lines.
- **Equivalents are code**: must be runnable snippets, not descriptions.
- **No equivalent**: write `// No direct equivalent — <reason>`.
- **Keep it tight**: WHAT and WHY are each one sentence max.
- **SYNTAX is rare**: only when a token/pattern would trip a learner.
