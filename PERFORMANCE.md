# Milestone 5 — Hell Run performance on Android

Measured on **2026-11-28**, using an **LG Q7 (LM-Q610.FG)** with a Mali-T860 GPU. Unity **6000.6.7f1**, Android **Development Build**, connected Unity Profiler, Deep Profile off. Target: **30 FPS / 33.33 ms**. Submission branch: `milestone-5`.

## Problem and measured cause

During a run, movement felt laggy and gameplay stayed around **20 FPS**, even though the code requested 30 FPS. The strongest evidence points to Android frame presentation/pacing on this phone. Lower rendering resolution, HDR off, a smaller ground texture, and unlit sprite materials reduced resource use but did not remove the roughly 50 ms frame cadence.

After disabling **Player Settings → Android → Resolution and Presentation → Optimized Frame Pacing**, the same phone reached **29.9 FPS**. The setting is saved as `androidUseSwappy: 0` in [ProjectSettings.asset](HellRun/ProjectSettings/ProjectSettings.asset). This was the biggest measured FPS improvement: **49.9% higher FPS**, or **33.3% less average frame time**, compared with the preceding test.

The Profiler's presentation waits are evidence of waiting, not proof that shaders alone consumed that time. For example, before the pacing change, frame 200 took 50.530 ms, including 38.325 ms in `WaitForTargetFPS` and about 40.186 ms in render-thread `Gfx.PresentFrame`. Afterward, frame 150 took 33.594 ms, including 23.013 ms in `WaitForTargetFPS`, with `Gfx.PresentFrame` about 3.981 ms. These are overlapping thread timings, so they must not be added together. Detailed GPU profiling was not available for this phone's graphics backend; the reported GPU frame counter is not treated as an isolated shader-cost measurement.

## Baseline and four optimization tests

All rows measure **Level gameplay**, excluding the main menu and scene-loading frames. Changes are cumulative. FPS is `1000 / mean frame ms`. GC Alloc is the mean bytes allocated per frame; the median was **0 B/frame in every row**. Texture memory is the median in MiB (bytes / 1,048,576), corresponding to the Profiler's MB display. Draw calls are recorded separately below and are not mislabeled as batches.

| Test | Configuration/change | Mean frame ms | Approx. FPS | Mean GC Alloc B/frame | Texture MiB |
|---|---|---:|---:|---:|---:|
| Baseline: `LGQ7-bEFORE` | Render Scale 1.0, HDR on, lit sprites, optimized pacing on | 49.823 | 20.1 | 479 | 58.09 |
| 1: `LGQ7-After` | Render Scale reduced to 0.75 | 50.129 | 19.9 | 134 | 46.41 |
| 2: `LGQ7-After2` | HDR off; ground image reduced to 512 × 512 | 50.236 | 19.9 | 341 | 41.98 |
| 3: `LGQ7-After3` | Render Scale 0.5; gameplay sprites use URP Sprite-Unlit-Default | 50.073 | 20.0 | 183 | 15.16 |
| **4: `LGQ7-PacingOff`** | **Optimized Frame Pacing disabled; target remains 30** | **33.402** | **29.9** | **59** | **15.16** |

Profiler automatic screenshots were disabled during the investigation before test 3. A separate `diagnostic.data` capture still averaged 49.826 ms with screenshots disabled, so screenshot capture alone did not explain the 20 FPS ceiling. Differences in texture memory across earlier captures include rendering settings, loaded assets, and profiling configuration; they are not attributed to one change in isolation.

### Closest before/after comparison for the main fix

| Metric | Before: After3, frames 20–340 | After: PacingOff, frames 20–280 |
|---|---:|---:|
| Frames analyzed | 321 | 261 |
| Mean frame time | 50.073 ms | **33.402 ms** |
| Median frame time | 49.842 ms | **33.208 ms** |
| 95th percentile frame time | 51.692 ms | **33.948 ms** |
| Slowest frame in analyzed range | 83.784 ms | **65.251 ms** |
| Main-thread active time, mean | 15.464 ms | 13.699 ms |
| Render-thread active time, mean | 4.792 ms | 4.486 ms |
| GC Alloc, mean / median | 183 / 0 B per frame | 59 / 0 B per frame |
| Draw calls, mean | 16.9 | 17.5 |
| Texture memory, median | 15.16 MiB | 15.16 MiB |

**Comparison limit:** the same phone, Development Build tooling, and Level gameplay were used, but these are independent runs with random encounters, not an identical seeded replay or a matched elapsed-time checkpoint. The final recording included alternating D/G attacks. This supports the observed improvement but does not fully satisfy a strict interpretation of “same moment.” A controlled repeat at the same elapsed run time would strengthen the submission. Occasional CPU spikes remain; this fix does not remove every hitch.

One remaining allocation source is `NormalAttack.Update`: the after capture's frame 150 attributes **56 B** to that method. This is separate from the main pacing fix and has not been optimized as part of this milestone change.

## Profiler screenshots

These are actual Unity Profiler window captures made after loading the saved Android recordings. Their selected-frame values differ from the range averages above. The source recordings identify them as player captures; the Editor toolbar can show its current connection rather than the original saved capture's device.

Before the pacing fix — `LGQ7-After3.data`, selected frame 200 (Profiler UI frame 201):

![Before: 50.53 ms frame, 15.2 MB texture memory](HellRun/ProfilerCaptures/LGQ7-PacingOn-Profiler.png)

After the pacing fix — `LGQ7-PacingOff.data`, selected frame 150 (Profiler UI frame 151):

![After: 33.59 ms frame, 15.2 MB texture memory](HellRun/ProfilerCaptures/LGQ7-PacingOff-Profiler.png)

## Fixes saved in this branch

- Main measured fix: disable Android Optimized Frame Pacing in `ProjectSettings.asset`.
- Supporting rendering changes: URP Render Scale **0.5**, HDR **off**, unlit gameplay sprite materials, camera shadows off, and the **512 × 512** ground texture. These were tested before the pacing change and did not alone raise FPS above 20.
- Deliberate cap: `Application.targetFrameRate = 30` already exists in [RunCrystalCounter.cs](HellRun/Assets/Scripts/Game/Crystals/RunCrystalCounter.cs). The dated rationale is in [DECISIONS.md](DECISIONS.md).
- Android Development Build and Autoconnect Profiler settings are saved in the Android build profile for reproducing measurements. Use a non-development build for release.

## Three biggest source textures: Android import settings

Ranked by original source pixel count across `Assets`, using `TextureImporter.GetSourceTextureWidthAndHeight`. This includes third-party example/UI assets; it is not a ranking of textures resident during gameplay.

| Texture | Source size | Max Size (default and Android) | Android override | Mipmaps |
|---|---:|---:|---|---|
| `Assets/TextMesh Pro/Examples & Extras/Textures/Sunny Days - Seamless.jpg` | 2048 × 1024 | 512 | ASTC 6×6, enabled | Off |
| `Assets/Alebardium/Bloodlines UI/Textures/Layout/Colors.png` | 2630 × 796 | 1024 | ASTC 6×6, enabled | Off |
| `Assets/Sprites/Menu/Infernal Fortress Amidst Lava and Chains.png` | 1672 × 941 | 1024 | ASTC 6×6, enabled | Off |

These explicit Android overrides were added **after** the recorded performance tests to complete the texture requirement. Their effect is not included in, or credited for, the measured pacing improvement. They require a new Android build and visual check on the phone.

## Reproduce and inspect

1. Use the LG Q7, the Android Development Build profile, and target 30 FPS. Leave Deep Profile off and Profiler automatic screenshot capture off for the closest pacing comparison.
2. Connect the Unity Profiler to the phone over USB, enter the Level scene, and record normal gameplay with D/G attacks. Exclude the menu and scene transition from the analyzed range. For a stricter repeat, use the same elapsed-time checkpoint, encounter setup, and input sequence for both runs.
3. In the Profiler, load the saved `.data` captures from [ProfilerCaptures](HellRun/ProfilerCaptures). They are stored with Git LFS; fetch LFS files after cloning.
4. The `*-summary.json` and `*-counters.json` exports contain the exact frame ranges and statistics used above. Baseline: frames **950–1270**; test 1: **235–555**; test 2: **196–516**; test 3: **20–340**; test 4: **20–280**. All exports use a 33.333 ms analysis budget even where older capture Highlights were configured to display 60 FPS.

Submission: the team submits its team name and repository link on Moodle, with this work on `milestone-5`. Do not commit to that branch after **2026-12-03 23:59**. Each team member must make their own contributions under their own Git identity.
