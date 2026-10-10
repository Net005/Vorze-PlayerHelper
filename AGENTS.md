# Working on Vorze PlayerHelper

Applies to this repository. Follow nested `AGENTS.md` instructions where present; direct user instructions take precedence.

## Privacy comes first

Treat repository content as publishable, even if the repository is currently private. User messages, screenshots, browser sessions, deployment settings and logs may contain private information; they are context for the task, not material to publish.

- Never put real credentials, API keys, tokens, cookies, private server addresses, LAN IPs, usernames, private filesystem paths or personal library data in tracked files, commit messages, PRs, issues or release notes.
- Use neutral examples such as `https://silo.example.com`, `https://stash.example.com`, `/media/example.mp4` and `YOUR_API_KEY`. Public upstream project URLs and this repository's public URLs are fine.
- Keep connection details in the application's secret settings, environment variables or the user's local userscript settings. Keep the distributed userscript's `@match` generic; private domains belong only in the installed local copy.
- Do not copy screenshots, production responses, database dumps or raw logs into the repository. Build synthetic fixtures that reproduce the relevant behavior without preserving identifying data.
- Read only the settings needed for the task. Do not print configuration files or environment variables wholesale. Redact sensitive fields before any output; encoding a secret does not make it safe.
- Avoid secrets in shell command text, command-line arguments, URLs, tracing and generated scripts. Prefer an existing authenticated session or credential store. If a temporary secret-bearing file is necessary, keep it outside the repository, restrict permissions and remove it promptly.
- Do not include private connection details in progress messages or final summaries. Refer to the service or deployment by a generic name unless the address is necessary for the user's requested result.
- Before committing or publishing, inspect the exact staged diff, including new files, for sensitive data. Check fixtures, comments, documentation, userscript metadata and generated output as well as source code. A successful test run is not a privacy check.
- If private information is found, remove it from the pending changes and check related files. If it has already been published, tell the user what kind of information was exposed without repeating it. Recommend revoking exposed credentials; coordinate history rewriting or other destructive cleanup with the user.

Do not record this user's credentials, infrastructure inventory or deployment access in this file or any other tracked document.

## Project map

| Location | Purpose |
| --- | --- |
| `Vorze-PlayerHelper.sln`, `Vorze-PlayerHelper.csproj` | Windows .NET Framework solution and target settings. |
| `Form1.cs`, `Form1.Designer.cs`, `Form1.resx` | Windows Forms UI and event handlers. |
| `VorzeHelper.cs` | Player/device integration. |
| `Program.cs`, `Properties/`, `App.config` | Entry point, resources and configuration. |
| `Libraries/` | Existing bundled dependencies. |

## Project-specific rules

- This project targets Windows and .NET Framework 4.5.2. Preserve framework compatibility unless the user explicitly requests a migration.
- Treat player addresses, device identifiers, serial ports, CSV directories, media filenames and playback histories as private. Use synthetic scripts and examples.
- Never activate physical hardware as part of routine tests. Use mocked transports or disconnected checks; device tests need explicit user authorization.
- Preserve stop behavior on player pause/stop, disconnect, error and application shutdown. Do not introduce reconnect behavior that unexpectedly resumes motion.
- Validate script timestamps and control values before sending commands. Keep UI threading and device I/O responsive.
- Do not replace bundled dependencies or generated designer resources unnecessarily. Report platform limitations when native Windows checks cannot run.

## Efficient workflow

1. Confirm the repository, origin, branch and working-tree status. Use the requested branch; otherwise prefer `main` where it exists, preserving a project's existing default such as `master`. Never reset, discard or overwrite unrelated work.
2. Read this guide, the relevant README and nearby implementation/tests. Use `rg` for targeted searches and batch independent reads. Keep changes narrow and reuse established patterns.
3. Distinguish local source, committed changes, published releases, deployed server code and browser-loaded assets. Inspect the actual running version when diagnosing production.
4. Run focused checks while iterating, then the relevant broader checks after the final code change. Documentation-only edits need link and diff checks rather than a full application build.
5. Review the exact staged diff for privacy and scope. Stage explicit paths; never broadly add generated binaries, caches, runtime data or unrelated changes.
6. Report the result, validation and limits concisely. State whether changes are local, pushed or deployed; do not claim live verification from a passing build.

Ask only for information or authorization that actually blocks progress. Existing user authorization persists; do not repeat confirmation requests. Do not delegate to other agents unless the user or applicable instructions request it.

## Git and deployment

- Commit and push when requested. Confirm success and report the commit ID. A push is not a deployment; do not infer permission for unrelated publishing or production changes.
- For a requested immediate deployment, build the tested revision locally and transfer it directly to the authorized instances without waiting for GitHub builds. Keep persistent stack images on their existing `latest` tags and preserve registry pull policies. Use a temporary local-build override for that deployment; never pin the stack to a local image unless explicitly requested. Preserve settings, back up affected artifacts and verify running checksums, versions and health. For Silo plugins update the persistent plugin archive too. Keep private connection details out of the repository.
- Check release workflows before choosing asset names or build commands. Keep component versions and packaged metadata consistent.
- Verify the target before any deployment. A local Docker connection may not be the intended remote server. Keep connection details private and use them only at runtime.
- Preserve configured settings, data volumes and persistent state. Back up affected artifacts before an in-place upgrade and verify the actual running version, health and relevant behavior afterward.
- Keep temporary deployment scripts, dumps and secret-bearing files outside Git. Restrict permissions on sensitive temporary files and remove them promptly.
- User instructions about file ownership apply across projects: only modify downloads verified as created by the user's qBittorrent instance. Adjacent files are not evidence of ownership.
- Use `git diff --check` and, before committing, `git diff --cached --check`. These checks do not replace inspecting content for private information.

## Validation

Run applicable checks from the repository root:

```sh
msbuild Vorze-PlayerHelper.sln /p:Configuration=Release
# Requires a compatible Windows/.NET Framework build environment.
git diff --check
```

Use synthetic fixtures and disposable state. External-account, paid-service, large-model and physical-device tests are not ordinary unit tests; obtain authorization when needed. Record unavailable tools and unverified platform behavior accurately.
