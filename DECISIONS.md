# Decision log

## 2026-11-28 — Target 30 FPS on Android

Options: target 30 FPS / target 60 FPS.
Chose: 30 FPS, as agreed during LG Q7 testing. A 33.33 ms frame budget is a more realistic target for this phone than the 16.67 ms needed for 60 FPS. Keep Android Optimized Frame Pacing disabled: the recorded gameplay improved from 50.073 ms (20.0 FPS) to 33.402 ms (29.9 FPS) after disabling it. This result applies to the LG Q7 and should be checked on other devices before release.
Implementation: `Application.targetFrameRate = 30` in `HellRun/Assets/Scripts/Game/Crystals/RunCrystalCounter.cs`; `androidUseSwappy: 0` in `HellRun/ProjectSettings/ProjectSettings.asset`.
AI: helped investigate the captures, ran the final phone recording, and drafted the measurements and wording. Ilay chose the target and disabled Optimized Frame Pacing.
Who: Ilay Fridman.

Add an entry every time the team makes a real choice: a tool, a design, a trade-off.
About 1–2 entries a week. Newest at the top. Write the reasoning yourselves; AI may fix the wording.

## Template
```
## YYYY-MM-DD — [The decision, in a few words]
Options: [A] / [B] / [C]
Chose: [what] — [why]
AI: [what the AI suggested, and what you did with it — or "not used"]
Who: [name(s)]
```

## Example
```
## 2026-11-20 — Save progress locally first
Options: PlayerPrefs / JSON file / Cloud Save
Chose: JSON file now, Cloud Save later
Why: works offline, easy to move later
AI: suggested a SQLite database — rejected, too heavy for one save file
Who: Yuval
```


## 31/10/2026 — Decided what the game is and how it will be played.
Options: Hell Run / Fantasy Run
Chose: I Chose Hell Run because fantasy run sounds regular, but escaping from hell with metal sounds much more fun
AI: AI mainly helped me organize the idea into a coherent game concept
Who: Ilay

## 11/11/2026 — decided API version is level 36
Options: 37/36
Chose: i chose 36 to allow some supports for older versions
Who: Ilay

## 14/11/2026 — Implemented Services using AI
Options: Code myself/Let AI Code
Chose: i let the ai create the code because that is how the industry works today and i need to practice code reviewing AI
Who: Ilay

## 14/11/2026 — Guitar data saved as assets
Options: save guitars as monobehaviour/c# script/scriptable object
Chose: allwos easy access to data and to create multiple variations of guitars
Who: Ilay
```
