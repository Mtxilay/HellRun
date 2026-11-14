# AGENTS.md — context for AI coding tools

AI tools read this file before working on the repo. Keep it short, accurate, and up to date.

If using Claude Code, also create a `CLAUDE.md` containing one line: `@AGENTS.md`.

## The game

Hell Run is a fast-paced, metal-themed roguelike rhythm runner for Android, played in portrait mode with one thumb. The player automatically runs through three lanes of Hell, dodging monsters, avoiding traps, and collecting loot while using color-matched guitar chords to fire lasers at enemies in the current lane. Playing chords in sync with heavy metal drum beats deals increased damage. hitting a monster with the wrong chord(wrong color) deals very weak damage.  while enemies that reach the player damage the guitar's durability until it breaks and the run ends. Players build Rage in a progress bar that can be consumed to unleash powerful attacks, face increasingly difficult enemies and bosses, and use Solo Mode during boss fights. Between runs, they can unlock and upgrade guitars with different stats and unique abilities. Procedurally arranged encounters make each run different as players progress through Hell and attempt to escape.

## Tech

- Engine: Unity 6 LTS, C#
- Target: Android (portrait)
- UI: Bloodlines - Dark UI
- Art style: 8-bit pixel art, top-down 2D

## Architecture

All gameplay areas share a single Level scene. Levels are procedurally generated, with each area featuring its own monsters, environment graphics, and difficulty progression.

Each area consists of 10 (Dynamic number that might cahnge) sublevels, which are variations of the current area with progressively stronger monsters, faster movement, and faster music. After completing all 10 sublevels, the player advances to the next area of Hell.

During runs, players collect monster parts and other resources that can be used to upgrade their guitars between runs.

Additional systems, configuration locations, and initialization order will be documented as they are implemented.

## Rules for AI

- Follow the existing folder structure and naming. Don't create new top-level folders.
- Don't invent APIs. If unsure about an SDK or Unity API, say so and refer to the documentation.
- Don't change Player Settings, package name, API levels, or build settings unless asked.
- Keep changes small and explain what changed and why.
- Never commit secrets (keystores, passwords, API keys).
- Always use the Ponytail plugin when available. Follow its principles of simple, minimal, maintainable code.
- Avoid unnecessary abstractions, dependencies, and overengineering.
- Reuse existing components and Unity functionality when possible.
- Don't implement additional features beyond the requested scope without approval.


## Conventions

Follow existing naming conventions, code style, and script locations.

Prefer small, focused components with clear responsibilities.

Additional conventions will be defined as development progresses.