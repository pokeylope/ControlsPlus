using System;
using System.Collections.Generic;
using System.Reflection;
using Assets.Scripts;
using Assets.Scripts.GridSystem;
using Assets.Scripts.Inventory;
using Assets.Scripts.Objects;
using Assets.Scripts.Objects.Entities;
using Assets.Scripts.Objects.Items;
using Assets.Scripts.UI;
using StationeersMods.Interface;
using UnityEngine;

namespace ControlsPlus;

[StationeersMod(ModId, DisplayName, Version)]
public sealed class ControlsPlusMod : ModBehaviour
{
    public const string ModId = "com.james.controlsplus";
    public const string DisplayName = "Controls Plus";
    public const string Version = "0.1.0";

    private const string ControlsGroupName = "Controls Plus";
    private const string SensorLensesControl = "Toggle Sensor Lenses";
    private const string TabletControl = "Toggle Tablet";

    private static readonly MethodInfo AddKeyMethod = typeof(KeyManager).GetMethod(
        "AddKey",
        BindingFlags.Static | BindingFlags.NonPublic,
        null,
        new[] { typeof(string), typeof(KeyCode), typeof(ControlsGroup), typeof(bool) },
        null);

    private KeyCode _sensorLensesKey = KeyCode.F7;
    private KeyCode _tabletKey = KeyCode.F8;

    public override void OnLoaded(ContentHandler contentHandler)
    {
        base.OnLoaded(contentHandler);

        try
        {
            RegisterControls();
            KeyManager.OnControlsChanged += RefreshBindings;
            RefreshBindings();
            LogMessage($"Loaded. Sensor lenses: {_sensorLensesKey}; tablet: {_tabletKey}.");
        }
        catch (Exception exception)
        {
            LogException("Failed to register controls", exception);
        }
    }

    private void Update()
    {
        if (!ControlsShouldFunction())
        {
            return;
        }

        if (_sensorLensesKey != KeyCode.None && KeyManager.GetButtonDown(_sensorLensesKey))
        {
            ToggleSensorLenses();
        }

        if (_tabletKey != KeyCode.None && KeyManager.GetButtonDown(_tabletKey))
        {
            ToggleTablet();
        }
    }

    private void OnDestroy()
    {
        KeyManager.OnControlsChanged -= RefreshBindings;
    }

    private static void RegisterControls()
    {
        if (AddKeyMethod == null)
        {
            throw new MissingMethodException(typeof(KeyManager).FullName, "AddKey");
        }

        ControlsGroup group = KeyManager.GetControlsGroup(ControlsGroupName);
        if (group == null)
        {
            group = new ControlsGroup(ControlsGroupName);
            KeyManager.AddGroupLookup(group);
        }

        AddControlIfMissing(SensorLensesControl, KeyCode.F7, group);
        AddControlIfMissing(TabletControl, KeyCode.F8, group);
        ControlsAssignment.RefreshState();
    }

    private static void AddControlIfMissing(string name, KeyCode defaultKey, ControlsGroup group)
    {
        if (KeyManager.GetKeyitem(name) == null)
        {
            AddKeyMethod.Invoke(null, new object[] { name, defaultKey, group, false });
        }
    }

    private void RefreshBindings()
    {
        _sensorLensesKey = KeyManager.GetKey(SensorLensesControl);
        _tabletKey = KeyManager.GetKey(TabletControl);
    }

    private static bool ControlsShouldFunction()
    {
        if (GameManager.GameState != GameState.Running || WorldManager.IsGamePaused || ConsoleWindow.IsOpen)
        {
            return false;
        }

        if (InputWindow.InputState != InputPanelState.None ||
            InputPrefabs.InputState != InputPanelState.None ||
            InputSourceCode.InputState != InputPanelState.None)
        {
            return false;
        }

        Human player = InventoryManager.ParentHuman;
        return player != null && player.IsLocalPlayer;
    }

    private static void ToggleSensorLenses()
    {
        Human player = InventoryManager.ParentHuman;
        DynamicThing lenses = player?.GlassesSlot?.Get();
        Interactable toggle = lenses?.InteractOnOff;

        if (toggle != null)
        {
            toggle.PlayerInteractWith();
        }
        else
        {
            PlayFailureSound();
        }
    }

    private static void ToggleTablet()
    {
        Human player = InventoryManager.ParentHuman;
        if (InventoryManager.Instance == null || player == null)
        {
            return;
        }

        Slot tabletSlot = FindTabletSlot(player);
        if (tabletSlot == null)
        {
            PlayFailureSound();
            return;
        }

        InventoryManager.SmartStow(tabletSlot);
    }

    private static Slot FindTabletSlot(Human player)
    {
        if (player.LeftHandSlot?.Get<Tablet>() != null)
        {
            return player.LeftHandSlot;
        }

        if (player.RightHandSlot?.Get<Tablet>() != null)
        {
            return player.RightHandSlot;
        }

        HashSet<long> visited = new HashSet<long>();
        foreach (Slot slot in EnumerateSlots(player, visited))
        {
            if (slot.Get<Tablet>() != null)
            {
                return slot;
            }
        }

        return null;
    }

    private static IEnumerable<Slot> EnumerateSlots(Thing thing, HashSet<long> visited)
    {
        if (thing == null || !visited.Add(thing.ReferenceId) || thing.Slots == null)
        {
            yield break;
        }

        foreach (Slot slot in thing.Slots)
        {
            if (slot == null)
            {
                continue;
            }

            yield return slot;

            DynamicThing occupant = slot.Get();
            foreach (Slot nestedSlot in EnumerateSlots(occupant, visited))
            {
                yield return nestedSlot;
            }
        }
    }

    private static void PlayFailureSound()
    {
        UIAudioManager.Play(UIAudioManager.ActionFailHash);
    }

    private static void LogMessage(string message)
    {
        Debug.unityLogger.Log(LogType.Log, $"[Controls Plus] {message}");
    }

    private static void LogException(string message, Exception exception)
    {
        Debug.unityLogger.Log(LogType.Error, $"[Controls Plus] {message}: {exception}");
    }
}
