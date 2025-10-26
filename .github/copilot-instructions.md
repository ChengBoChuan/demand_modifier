# Copilot Instructions for Cities: Skylines 2 - Demand Control (.NET 8)

## Project Overview
This is a **Cities: Skylines 2 mod** that manipulates in-game demand systems using Harmony patches. The project targets **.NET 8** and integrates with the game's Unity ECS (Entity Component System) architecture.

**Repository Structure**: Designed as a multi-mod repository (currently contains `InfiniteDemand/` mod, extensible for future demand-related mods).

## Architecture & Key Patterns

### Harmony-Based Patching Architecture
All mods in this repository use **runtime method interception** via `HarmonyLib`:

- **Entry Point**: `IMod.OnLoad()` calls `harmony.PatchAll()` to auto-discover patches
- **Patch Pattern**: `[HarmonyPatch(typeof(TargetSystem), "OnUpdate")]` attributes on static classes
- **Prefix Patches**: Execute before original method, allowing value override before game logic runs
- **Field Access**: Use `AccessTools.FieldRefAccess<T, TField>("m_PrivateFieldName")` to access game's private fields

**Example from `DemandSystemPatch.cs`:**
```csharp
[HarmonyPatch(typeof(CommercialDemandSystem), "OnUpdate")]
public class CommercialDemandSystemPatch
{
    private static AccessTools.FieldRef<CommercialDemandSystem, NativeValue<int>> BuildingDemandRef =
        AccessTools.FieldRefAccess<CommercialDemandSystem, NativeValue<int>>("m_BuildingDemand");

    static void Prefix(CommercialDemandSystem __instance) {
        BuildingDemandRef(__instance).value = 255; // Max demand
    }
}
```

### Unity ECS Integration
CS2 uses Unity's Entity Component System - patches intercept system update cycles:

- **Systems to Patch**: `CommercialDemandSystem`, `IndustrialDemandSystem`, `ResidentialDemandSystem`
- **Data Structures**: Use `NativeValue<T>` and `NativeArray<T>` (thread-safe Unity types)
- **Update Cycle**: Patches inject into `OnUpdate()` methods that run every simulation tick
- **Import Namespaces**: `Unity.Entities`, `Unity.Collections`, `Unity.Mathematics`, `Colossal.Collections`

### Demand System Internals
Critical knowledge for working with CS2 demand:

- **Value Range**: 0-255 (位元組 values) - 255 = maximum demand
- **Residential Complexity**: Uses `int3` (low/medium/high density) + 11 demand factors (see `DemandFactor` enum)
- **Industrial Fields**: Separate demands for industrial/storage/office buildings (`m_IndustrialBuildingDemand`, etc.)
- **Simulation Lag**: Changes don't display immediately - game must run simulation cycles to show effect

## .NET 8 Specific Features

### Language Features Used
- **File-Scoped Namespaces**: Use `namespace InfiniteDemand;` instead of block syntax
- **Collection Expressions**: Use `[element1, element2, ...]` instead of `new[] { element1, element2, ... }`
- **Nullable Reference Types**: Enabled via `<Nullable>enable</Nullable>`
- **Implicit Usings**: Enabled via `<ImplicitUsings>enable</ImplicitUsings>`
- **Latest Language Version**: Set via `<LangVersion>latest</LangVersion>`

### Modern C# Patterns
```csharp
// File-scoped namespace
namespace InfiniteDemand;

// Collection expression
private static DemandFactor[] Factors =
[
    DemandFactor.StorageLevels,
    DemandFactor.EducatedWorkforce,
    // ... more items
];
```

## Build & Development Setup

### Prerequisites
**CRITICAL**: Environment variable `CSII_TOOLPATH` must be set (User-level) pointing to CS2 mod tools directory.

The `.csproj` imports required props/targets from this path:
```xml
<Import Project="$([System.Environment]::GetEnvironmentVariable('CSII_TOOLPATH', 'EnvironmentVariableTarget.User'))\Mod.props" />
<Import Project="$([System.Environment]::GetEnvironmentVariable('CSII_TOOLPATH', 'EnvironmentVariableTarget.User'))\Mod.targets" />
```

**Additional Requirements**:
- **.NET 8 SDK**: Required for building .NET 8 projects

### Building
```powershell
# 標準建構
msbuild CSL2DemandControl_NET8.sln /p:Configuration=Release

# 偵錯建構
msbuild CSL2DemandControl_NET8.sln /p:Configuration=Debug

# Using dotnet CLI (alternative)
dotnet build CSL2DemandControl_NET8.sln -c Release
```

**Game References**: All game DLLs (Game.dll, Colossal.*.dll, Unity.*.dll) marked `<Private>false</Private>` - not copied to output.

### Publishing to Paradox Mods
Uses MSBuild publish profiles with custom PDX publisher integration:

1. **Credentials**: Store in `%USERPROFILE%\pdx_account.txt` (2 lines: email, then password) - **NEVER commit this file**
2. **Metadata**: Edit `Properties\PublishConfiguration.xml` (display name, description, tags, changelog)
3. **First Publish**: Use `PublishNewMod.pubxml` profile (sets `ModPublisherCommand=Publish`)
4. **Updates**: Use `PublishNewVersion.pubxml` (requires `<ModId>` in PublishConfiguration.xml, sets `ModPublisherCommand=NewVersion`)

**Version Sync Required**: Keep `AssemblyVersion` (.csproj) and `ModVersion` (PublishConfiguration.xml) synchronized.

## Project Conventions

### File Organization
- **Mod Entry Point**: `<ModName>Mod.cs` implementing `IMod` interface
- **Patches**: `DemandSystemPatch.cs` (multiple patch classes in one file - grouped by system)
- **Metadata**: `Properties/PublishConfiguration.xml`, `Properties/Thumbnail.png`
- **Profiles**: `Properties/PublishProfiles/*.pubxml`

### Naming Conventions
- **Namespace**: Matches mod folder name (e.g., `InfiniteDemand`) - use file-scoped syntax
- **Patch Classes**: `<SystemName>Patch` (e.g., `ResidentialDemandSystemPatch`)
- **Field Refs**: Descriptive names ending in `Ref` (e.g., `BuildingDemandRef`, `HouseholdDemandRef`)
- **Logger**: Static field `log` in mod class: `LogManager.GetLogger($"{nameof(Namespace)}.{nameof(ModClass)}")`

### Logging Pattern
```csharp
public static ILog log = LogManager.GetLogger($"{nameof(InfiniteDemand)}.{nameof(InfiniteDemandMod)}")
    .SetShowsErrorsInUI(false); // Prevents UI spam for players
```

## Common Tasks

### Adding a New Demand System Patch
1. Identify target system class (e.g., `ServiceDemandSystem`)
2. Add new patch class with `[HarmonyPatch(typeof(TargetSystem), "OnUpdate")]`
3. Use ILSpy/dnSpy to find private field names (prefixed with `m_`)
4. Create static field refs with `AccessTools.FieldRefAccess<SystemType, FieldType>("m_FieldName")`
5. Implement `Prefix` method with `__instance` parameter
6. Set demand values to 255
7. Harmony auto-discovers via `PatchAll()` - no manual registration needed

### Debugging
- **In-Code Logging**: `InfiniteDemandMod.log.Info("message")`
- **Game Logs**: `%AppData%\..\LocalLow\Colossal Order\Cities Skylines II\Logs\`
- **Visual Testing**: Speed up game time (+++++) to see demand changes faster
- **Reflection Issues**: Verify field names haven't changed in game updates (use dnSpy on Game.dll)

### Extending with New Mods
This repository supports multiple mods:
1. Create new folder at root (e.g., `DemandModifier/`)
2. Add new `.csproj` following `InfiniteDemand.csproj` pattern (target `net8.0`)
3. Include in solution file
4. Create separate `Properties/PublishConfiguration.xml`
5. Each mod published independently to Paradox Mods

## Critical Dependencies
- **Lib.Harmony** (2.4.1): NuGet 套件 for runtime patching
- **Game DLLs**: Not in repository - resolved via CSII_TOOLPATH imports
- **Unity ECS 套件**: Provided by game installation (Entities, Collections, Mathematics)
- **.NET 8 Runtime**: Required for execution

## Differences from .NET Framework 4.7.2 Version

### Removed Dependencies
- **Microsoft.QualityTools.Testing.Fakes**: Removed - not needed for this mod

### Project File Changes
- `TargetFramework`: Changed from `net472` to `net8.0`
- Added `<LangVersion>latest</LangVersion>`
- Added `<Nullable>enable</Nullable>`
- Added `<ImplicitUsings>enable</ImplicitUsings>`

### Code Modernization
- File-scoped namespaces throughout
- Collection expressions for array initialization
- Modern C# language features

## Troubleshooting
- **Build Fails**: Verify `CSII_TOOLPATH` environment variable is set (User-level, not System)
- **.NET 8 Not Found**: Install .NET 8 SDK from https://dotnet.microsoft.com/download
- **Mod Not Loading**: Check `Player.log` for exceptions; verify Harmony patches applied; game may require specific .NET runtime
- **Demand Not Changing**: Game requires simulation time - pause and speed up (+++++); check other limiting factors (citizens, workplaces)
- **Publish Fails**: Verify `pdx_account.txt` exists and contains valid credentials; check `PublishConfiguration.xml` has `<ModId>` for updates
- **Runtime Compatibility**: Ensure Cities: Skylines 2 supports .NET 8 mods (may require game update or launcher configuration)
