## Summary

Describe the user-visible change and why it is needed.

## Validation

- [ ] `dotnet build --configuration Release` succeeds without warnings or errors.
- [ ] `scripts/ValidateRepository.ps1` succeeds.
- [ ] Both controls appear and can be rebound under Controls Plus.
- [ ] Sensor-lenses on/off behavior was tested.
- [ ] Tablet and Advanced Tablet equip/stow behavior was tested.
- [ ] Construct and Deconstruct were tested with one- and two-item stages,
      missing requirements, and full hands/inventories.
- [ ] Pause, text-entry, full-hand, and full-inventory edge cases were tested.
- [ ] Multiplayer client behavior was tested, or the reason it was not is documented below.

## Compatibility notes

List affected game APIs, control-name changes, or interactions with other shortcut and inventory mods.
