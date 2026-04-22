markdown# Trilingual Learning Mode (Active Recall First)

## Purpose
Force prediction before explanation. You don't learn by reading — you learn by
being wrong first, then seeing why.

---

## HOW TO USE THIS WITH CURSOR (or Claude)

Paste this at the start of any session:

```
Follow TRILINGUAL_ACTIVE_RECALL.md for every new concept line.
Do NOT show me the answer first.
Ask me to predict before revealing.
```

---

## Session Flow (per new-concept line)

### STEP 1 — Cursor/Claude asks you:
> "What does this line do?"
> "What's the Python equivalent?"
> "What's the R equivalent?"
> *(pause — you answer before anything is revealed)*

### STEP 2 — Then and only then, show:
```csharp
// actual C# line
// Python: 
// R: 
// WHAT: one sentence
// WHY: one sentence
// SYNTAX: (only if a token would confuse a cross-language learner)
```

---

## Rules
- Never reveal answers before prediction attempt
- Only apply this to lines introducing new concepts (skip boilerplate repeats)
- WHAT and WHY = 1 sentence max
- Equivalents must be real runnable code — never descriptions
- If no equivalent exists: `// No direct equivalent — [reason]`

---

## End-of-File Checkpoint (run after every completed file)

Cursor/Claude prompts:

1. **Memory rebuild** — "Close the file. Rewrite it from memory in C#."
2. **Python port** — "Now rewrite the same logic in Python without looking."
3. **R port** — "Now rewrite it in R without looking."
4. **Diff review** — Open your version vs original. Note every diff. Explain each one.

---

## Quick Cursor Paste (save this somewhere fast)

```
Follow TRILINGUAL_ACTIVE_RECALL.md.
For each new-concept line: ask me WHAT it does + Python/R equivalents BEFORE showing answers.
After the file: run the 3-step memory rebuild (C# from memory → Python port → R port).
Format answers as: C# line / Python: / R: / WHAT: / WHY: / SYNTAX: (if needed)
```

Next steps:

Commit: git add TRILINGUAL_ACTIVE_RECALL.md && git commit -m "docs: add active recall learning protocol
