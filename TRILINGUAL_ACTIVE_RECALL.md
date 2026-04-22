# Trilingual Active Recall Protocol

## Goal
Predict first, then verify. You answer before any “solution” is shown.

## Per new-concept line
Ask these questions and **wait for the user’s answers**:
1. What does this line do?
2. What’s the Python equivalent?
3. What’s the R equivalent?

Then reveal the line in the standard format:

```csharp
// <C# line>
// Python: <runnable code>
// R: <runnable code>
// WHAT: <1 sentence>
// WHY: <1 sentence>
// SYNTAX: <only if needed>
```

## Rules
- **No early reveals**: never show the annotated line before prediction.
- **Scope**: only “new concept” lines (skip repeated boilerplate).
- **Equivalents are code**: runnable snippets, never descriptions.
- **No equivalent**: `// No direct equivalent — <reason>`.
- **Short**: WHAT and WHY are each one sentence max.

## End-of-file checkpoint
1. Rewrite the file from memory in C# (no peeking).
2. Port the same logic to Python (no peeking).
3. Port the same logic to R (no peeking).
4. Diff vs the original and explain every difference.

## Prompt snippet (paste into chat)

```
Follow TRILINGUAL_ACTIVE_RECALL.md.
For each new-concept line: ask me what it does + Python/R equivalents BEFORE revealing.
After the file: run the end-of-file checkpoint (C# memory rebuild → Python port → R port → diff review).
```
