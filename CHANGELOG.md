# Changelog

All notable changes to Controls Plus are documented here.

The format follows [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project uses [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## Unreleased

## 0.4.0 - 2026-08-15

### Added

- Exact inventory items can be assigned to number-row hotkeys `1` through `0`
  by hovering their tile while cursor control is active.
- Pressing a bound number during normal play toggles that item between a usable
  hand and its remembered inventory slot.
- When both hands are occupied, Controls Plus temporarily stows one held item,
  equips the bound item, and restores the displaced item when the same hotkey is
  pressed again. Temporary stowing preserves the exact item rather than merging
  it into another stack.
- Assigning a number to a new item replaces its previous binding; assigning the
  same number to the same item clears it.
- Bindings persist locally per world and character using Stationeers' stable
  world and item reference IDs.

### Changed

- A number with an item assigned takes priority over the vanilla equipment-slot
  shortcut using that number. Unassigned numbers retain vanilla behavior.

## 0.3.0 - 2026-08-15

### Added

- Smart Stow remembers the inventory slot from which an item was equipped and
  returns the item there when that slot remains available.
- Stackable items can merge across multiple partial stacks when prefab and
  colour both match.
- Any remainder is passed to Stationeers' normal Smart Stow behavior.
- Native server-routed move and merge operations preserve multiplayer support.

### Changed

- Single-item construction stages prefer an already-empty hand, preserving an
  unrelated item held in the other hand.

## 0.2.4 - 2026-08-15

### Fixed

- Simulated swaps now validate slot compatibility independently of current
  occupancy, allowing a held required material and an inventory tool to be
  arranged correctly regardless of which hand is active.

## 0.2.3 - 2026-08-15

### Fixed

- Tool-use costs are no longer treated as stack counts. Quantity validation is
  now limited to stackable construction materials, fixing deconstruction tools
  whose durability or charge use exceeds one.

## 0.2.2 - 2026-08-15

### Fixed

- Construct and Deconstruct can now use direct hand/inventory swaps when no
  empty stow slot is available, avoiding deadlocks after recovered materials
  refill the player's inventory.

## 0.2.1 - 2026-08-15

### Fixed

- Construct and Deconstruct now use Stationeers' resolved cursor target, fixing
  structures whose active collider changes between build states, including
  initial iron frames and affected devices.

## 0.2.0 - 2026-08-15

### Added

- A Construct shortcut that equips the tools and materials required by the next
  build state of the structure under the crosshair.
- A Deconstruct shortcut that equips the current build state's removal tool.
- Preflight selection across nested inventories, compatible replacement tools,
  required stack quantities, both hands, and available stow destinations.
- Ordered native inventory moves suitable for multiplayer clients.

## 0.1.0 - 2026-08-14

### Added

- A rebindable shortcut for toggling equipped sensor lenses on and off.
- A rebindable shortcut for equipping or stowing Tablet and Advanced Tablet
  items through Stationeers' native Smart Stow behavior.
- A dedicated Controls Plus group in the normal Controls settings.
- Input guards for pause, console, prefab, text, and IC source-code entry.
- Client-side multiplayer support using native interaction and inventory paths.
