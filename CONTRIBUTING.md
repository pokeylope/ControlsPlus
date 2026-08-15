# Contributing

Bug reports and focused pull requests are welcome.

## Development setup

1. Install Stationeers, BepInEx 5.4, and StationeersLaunchPad 0.5.
2. Clone this repository into a convenient development directory.
3. Build with `dotnet build --configuration Release`, overriding
   `StationeersPath` if needed.
4. Copy or link the LaunchPad payload into
   `Documents\My Games\Stationeers\mods` if the repository is elsewhere.

Stationeers and its managed assemblies are proprietary dependencies and must
not be committed.

## Before submitting a change

- Run a Release build with zero warnings and errors.
- Run `powershell -NoProfile -ExecutionPolicy Bypass -File scripts/ValidateRepository.ps1`.
- Confirm both controls appear and can be rebound in the Controls Plus group.
- Test sensor lenses in their on and off states and with no lenses equipped.
- Test normal and Advanced Tablets in each hand and in nested inventories.
- Test one- and two-item construction stages, compatible replacement tools,
  deconstruction tools, incomplete stacks, and missing requirements.
- Test full hands, full inventories, pause, console, text input, and IC editing.
- Test on a multiplayer client connected to a listen or dedicated server.

## Bug reports

Include the Stationeers build, BepInEx and StationeersLaunchPad versions,
Controls Plus version, configured bindings, reproduction steps, and the smallest
relevant log excerpt.
