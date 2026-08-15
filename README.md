# Controls Plus

Controls Plus is a lightweight, client-side
[StationeersLaunchPad](https://stationeerslaunchpad.github.io/) mod that adds
four convenient controls to Stationeers:

- **Toggle Sensor Lenses** turns worn sensor lenses on or off. Default: `F7`.
- **Toggle Tablet** equips a Tablet or Advanced Tablet from your inventory, or
  stows it when already held. Default: `F8`.
- **Construct** equips the tools and materials required by the next construction
  stage of the structure under the crosshair. Default: `F9`.
- **Deconstruct** equips the tool required to remove the current construction
  stage of the structure under the crosshair. Default: `F10`.

Both shortcuts appear in the **Controls Plus** section of Stationeers' normal
Controls settings and can be rebound like vanilla controls.

## Requirements

- Stationeers
- BepInEx 5.4.23.3
- StationeersLaunchPad 0.5.0

Version 0.2.0 targets the Stationeers build current on 15 August 2026. The mod
adds no items, recipes, prefabs, or save data.

## Installation

### Steam Workshop

1. Install BepInEx and StationeersLaunchPad.
2. Subscribe to [Controls Plus](https://steamcommunity.com/sharedfiles/filedetails/?id=3783237133)
   in the Steam Workshop.
3. Enable the mod through LaunchPad and restart Stationeers.

### Manual release

1. Download the release archive.
2. Extract it into `Documents\My Games\Stationeers\mods`.
3. Confirm the resulting layout contains
   `ControlsPlus\About\About.xml` and `ControlsPlus\ControlsPlus.dll`.
4. Enable the mod through LaunchPad and restart Stationeers.

## Usage

Open **Settings → Controls → Controls Plus** to view or change the bindings.

### Toggle Sensor Lenses

The shortcut operates the normal on/off interaction on the item currently in
the player's glasses slot. It plays the ordinary failure sound when no
switchable sensor lenses are equipped.

### Toggle Tablet

When a Tablet or Advanced Tablet is in either hand, the shortcut uses the
game's Smart Stow behavior to return it to inventory. Otherwise, it searches
the player's belt, suit, backpack, and nested inventories and equips the first
tablet it finds. If both hands are occupied or no suitable inventory slot is
available, Stationeers handles the action exactly like an unsuccessful Smart
Stow operation.

### Construct

Aim at an incomplete structure and press the shortcut. Controls Plus reads the
next vanilla or modded `BuildState` and equips its required item or pair of
items into the active and off hand. For example, a stage requiring iron sheets
and a welding torch equips both when suitable items are available.

Compatible replacement tools are accepted through Stationeers' native
construction checks. Required quantities must be present in a single usable
stack. If any requirement is missing, a held item cannot be stowed, or the
crosshair is not over a constructible structure, nothing is moved.

### Deconstruct

Aim at a structure and press the shortcut to equip the tool required to remove
its current build stage. Missing tools and invalid targets are silent no-ops.

Construct and Deconstruct only equip items; they never perform the actual
construction or deconstruction interaction.

The shortcuts do not fire while the game is paused, the console is open, or a
text, prefab, or IC source-code input is active.

## Multiplayer

Controls Plus is client-side. It invokes Stationeers' normal player interaction
and inventory movement paths, so a dedicated server and other players do not
need the mod installed.

## Compatibility

Other shortcut or inventory mods can coexist with Controls Plus. Avoid assigning
the same physical key to overlapping actions. In particular, shortcut suites
that already provide glasses, tablet, build, or unbuild actions may duplicate
these features.

## Building

The project targets `netstandard2.1` and references the locally installed game,
BepInEx, and StationeersLaunchPad assemblies. The default game path is:

```text
D:\SteamLibrary\steamapps\common\Stationeers
```

Override it when necessary:

```powershell
dotnet build .\ControlsPlus.csproj -c Release -p:StationeersPath="D:\path\to\Stationeers"
```

Create a distributable archive with:

```powershell
powershell -NoProfile -ExecutionPolicy Bypass -File .\scripts\Package.ps1
```

The package is written to `artifacts/ControlsPlus-<version>.zip`.

## Source and contributions

Source code and issue tracking are available at
[jameslkingsley/ControlsPlus](https://github.com/jameslkingsley/ControlsPlus).
Bug reports and focused pull requests are welcome; see
[CONTRIBUTING.md](CONTRIBUTING.md).

## License

Controls Plus is released under the [MIT License](LICENSE).
