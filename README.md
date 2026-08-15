# Controls Plus

Controls Plus is a lightweight, client-side
[StationeersLaunchPad](https://stationeerslaunchpad.github.io/) mod that adds
four convenient controls, item hotkeys, and a more thoughtful Smart Stow to
Stationeers:

- **Toggle Sensor Lenses** turns worn sensor lenses on or off. Default: `F7`.
- **Toggle Tablet** equips a Tablet or Advanced Tablet from your inventory, or
  stows it when already held. Default: `F8`.
- **Construct** equips the tools and materials required by the next construction
  stage of the structure under the crosshair. Default: `F9`.
- **Deconstruct** equips the tool required to remove the current construction
  stage of the structure under the crosshair. Default: `F10`.

All four shortcuts appear in the **Controls Plus** section of Stationeers' normal
Controls settings and can be rebound like vanilla controls.

## Requirements

- Stationeers
- BepInEx 5.4.23.3
- StationeersLaunchPad 0.5.0

Version 0.4.0 targets the Stationeers build current on 15 August 2026. The mod
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

For a stage requiring only one item, Controls Plus uses an already-empty hand
when possible and leaves an unrelated item in the other hand untouched. If the
required item is already held, it remains in its current hand.

### Deconstruct

Aim at a structure and press the shortcut to equip the tool required to remove
its current build stage. Missing tools and invalid targets are silent no-ops.

Construct and Deconstruct only equip items; they never perform the actual
construction or deconstruction interaction.

The shortcuts do not fire while the game is paused, the console is open, or a
text, prefab, or IC source-code input is active.

### Enhanced Smart Stow

The normal Smart Stow control now remembers where an item came from. When the
original slot is still empty and compatible, the held item returns to that exact
slot.

If the original slot is unavailable—or the item was acquired directly into a
hand—stackable items are merged into existing stacks only when both their prefab
and colour match. Controls Plus can fill several partial stacks in one action.
Any remaining item is then handled by Stationeers' normal Smart Stow logic.

Original-slot memory lasts for the current play session. Moving an item into a
hand again records its latest inventory origin.

### Item hotkeys

Activate cursor control, hover an occupied inventory tile, and press a number
from `1` through `0`. Press that number during normal play to equip the exact
bound item; press it again while the item is held to Smart Stow it back to its
remembered slot. Item bindings do not add visual indicators to inventory tiles.

If the active hand is occupied but the other hand is empty, Controls Plus uses
the empty hand and makes it active. If both hands are occupied, Controls Plus
temporarily stows the active-hand item and equips the bound item in its place.
Pressing the same hotkey again stows the bound item and restores the displaced
item. If the active item cannot be safely stored, the other hand is tried; if
neither can be preserved, the action is a silent no-op. Temporary stowing does
not merge stackable items, ensuring the exact displaced item can be restored.

Assigning a number to another item replaces the old binding, and pressing the
same number over the same item clears it.

The number-row bindings are saved locally for the current world and character,
and follow the exact item instance as it moves or across game restarts. Assigned
numbers take priority over vanilla equipment-slot shortcuts; number keys without
an item binding retain their vanilla behavior. A binding is cleared automatically
if its item has been destroyed when the hotkey is next used.

## Multiplayer

Controls Plus is client-side. It invokes Stationeers' normal player interaction
and server-routed inventory movement and stack-merging paths, so a dedicated
server and other players do not need the mod installed.

## Compatibility

Avoid assigning the same physical key to overlapping shortcut actions. Shortcut
suites that already provide glasses, tablet, build, or unbuild actions may
duplicate these features.

Disable or remove **Inventory Tweaks** when using Controls Plus 0.3.0 or later.
Both mods replace Smart Stow, so running them together creates conflicting
behavior. Controls Plus logs a warning if it detects Inventory Tweaks at startup.

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
