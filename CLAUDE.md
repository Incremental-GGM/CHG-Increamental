# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project

Unity 2D incremental/tower-defense prototype ("bow defends the center, enemies walk inward, gold buys upgrades").
Unity **6000.5.10f1** (Unity 6.5), Universal RP 2D, Input System package present but unused.
Comments, commit messages, and `DisplayName` values are in Korean — keep that convention when editing existing files.

## Build / run

There is no CLI test suite, no `.asmdef`, and no lint config. All 17 scripts compile into the single
`Assembly-CSharp` assembly, which Unity regenerates — **never hand-edit `Assembly-CSharp.csproj`**.
`Library/`, `Temp/`, `obj/`, `Logs/` are generated (see `.gitignore`).

The editor on this machine lives at `C:\unity\6000.5.10f1\Editor\Unity.exe`.

```powershell
# Compile-check without opening the GUI (writes compiler errors into the log)
& "C:\unity\6000.5.10f1\Editor\Unity.exe" -batchmode -quit -projectPath "C:\Fork\CHG-Increamental" -logFile - 

# Headless build (Windows profile: Assets/Settings/Build Profiles/Windows.asset)
& "C:\unity\6000.5.10f1\Editor\Unity.exe" -batchmode -quit -projectPath "C:\Fork\CHG-Increamental" -buildWindows64Player "Build\game.exe" -logFile -
```

Batch mode fails while the GUI holds the project lock. Compiler errors from a GUI session end up in
`Logs/Editor.log`, which is the fastest way to verify a change compiled.

`com.unity.test-framework` is installed but no test assembly exists; adding tests means creating an
`Assets/Tests/` folder with a test `.asmdef` first.

## Where things live

`Assets/` uses numeric prefixes as a fixed ordering convention: `00.Scenes`, `01.Scripts`,
`03.GameModule` (prefabs + animation), `04.Graphics`. Put new files in the matching bucket.

Everything runs in **one scene**, `Assets/00.Scenes/SampleScene.unity` — the only scene in build settings.

## Architecture

### Managers are scene-resident singletons
Every manager derives from `MonoSingleton<T>` (`Assets/01.Scripts/MonoSingleton.cs`). Its `Instance`
getter *silently creates an empty GameObject* with the component if none is in the scene, so a manager
whose `[SerializeField]` references were meant to be wired in the Inspector will come up with null
fields instead of throwing. When a manager appears to do nothing, check it actually exists in
`SampleScene` rather than having been auto-spawned.

Managers: `GoldManager`, `HealthManager`, `RunManager` (namespace `Manager`) and `UpgradeManager`
(namespace `_01.Scripts.CoreSystem.Manager`). Namespaces across the project are inconsistent
(`Manager`, `Upgrade`, `_01.Scripts.Upgrade`, global) — match the file you are editing.

### Cross-system wiring lives in the scene, not in code
Manager→UI and manager→manager hookups are `UnityEvent` persistent calls serialized inside
`SampleScene.unity` / prefabs, so they are invisible to grep over `.cs` files:

- `GoldManager.OnGoldChanged(BigNumber)` → `UI.GoldInterface.GoldTextChange`
- `HealthManager.OnHealthChange(int)` → `UI.HealthInterface.HandleHealthChange`
- `HealthManager.OnDeath` → `RunManager.HandleRestart`
- ReStart Btn `onClick` → `RunManager.Restart`

**Renaming one of these methods or changing its parameter type breaks the link with no compiler error.**
Either keep the signature or re-wire in the Inspector (and say so to the user).

The code-side event, `UpgradeManager.OnStatChanged(string id, int level)`, is a plain C# `event` — `Bow`
subscribes in `Awake` and writes the value into its own `statsDic`.

### The string-key stat contract
An upgrade's `Id` (authored in the Inspector on `UpgradeManager._defaultUpgradeData`: currently `Damage`,
`AttackSpeed`, `DefaultUpgrade`) must match:
- the key `Bow.statsDic` reads (`statsDic["Damage"]` feeds `Bow.Damage`), and
- the `Key` field on the `UpgradeBtn` prefab/instance driving that button.

Adding a stat therefore means touching the Inspector data, `Bow`'s dictionary, and the button — not just code.

### `UpgradeData` is a mutable struct, and that is the main trap here
`UpgradeData` (`Assets/01.Scripts/Upgrade/UpgradeData.cs`) is a `struct` with an `event` field, stored in
a `List<UpgradeData>` and copied into `UpgradeManager._upgradeDataDict`. Consequences currently visible in
the code:

- `TryGetValue(key, out var data)` yields a **copy**; `data.CurrentLevel++` and `data.InitialCost +=` in
  `UpgradeManager.TryUpgrade` never reach the dictionary entry.
- `UpgradeManager.GetUpgradeData` returns a copy, so `UpgradeBtn.Start`'s `_upgradeData.OnStatChanged +=`
  subscribes to a copy's event that nothing will ever raise.
- `GoldManager.TryBuy` is called *before* the max-level/prerequisite checks in `TryUpgrade`, so gold is
  spent on rejected upgrades. The prerequisite loop also `&&`s a `ContainsKey` check against an indexer on
  the same key, so an unknown prerequisite id throws `KeyNotFoundException` instead of rejecting the buy.

Before "fixing" behaviour in this area, decide with the user whether `UpgradeData` becomes a class /
`ScriptableObject` or the dictionary gets written back explicitly — patching symptoms one call site at a
time will not converge.

### BigNumber
`Assets/01.Scripts/CoreSystem/BigNumber.cs` — `[Serializable] struct` of `double mantissa` + `long exponent`,
used for all gold and upgrade costs so it shows up as two Inspector fields. It defines only
`+ - *`, `>` and `<`: there is no division, no `==`/`>=`/`<=`, and `+` does **not** renormalize the mantissa
back into `[1,10)`, so values drift away from scientific-notation form and `ToString()` (`"1.234e5"`)
reflects that. Do not assume a normalized mantissa; `Mantissa <= 0` is the idiom used for "not positive"
(e.g. `GoldManager.AddGold`).

### Run loop
`EnemySpawner` runs waves from a serialized `List<WaveData>`, repeating the last entry once past the end,
and tracks spawned enemies so `ResetWave` can clear them. `RunManager.EndRunning` is the global pause gate
— `Bow.Update` and the spawner both check it. Death path: `Enemy` → `Bow.OnCollisionEnter2D` →
`HealthManager.TakeDamage` → `OnDeath` → `RunManager.HandleRestart` (shows upgrade panel, stops waves);
`RunManager.Restart` resumes. Enemies are found by the `"Enemy"` tag, not by the `_whatIsEnemy` LayerMask
fields, which are serialized but unused.
