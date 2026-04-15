# Project Work Rules

- Use Korean for user-facing replies.
- Keep answers short and summarize command output instead of pasting long logs.
- Prefer small, scoped patches. Avoid unrelated refactors.
- Treat `Biden/Func/Macro.cs` as the primary macro logic entry point unless the task points elsewhere.
- Inspect source files first: `Biden/Func`, `Biden/Model`, `Biden/ViewModel`, `Biden/View`, and `Biden/*.xaml`.
- Avoid reading generated or dependency folders unless necessary: `.vs`, `Biden/.vs`, `Biden/bin`, `Biden/obj`, and `packages`.
- When reporting completion, include only changed files, cause, and build/test status.

