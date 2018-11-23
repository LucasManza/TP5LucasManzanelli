using System.Collections.Generic;
using controller;
using UnityEngine;

public class KeysConfiguration
{
    public enum Action
    {
        None,
        Fired,
        MoveUp,
        MoveDown,
        MoveRight,
        MoveLeft,
    }

    public Player.PlayerID PlayerId;
    public Dictionary<KeyCode, Action> KeyCodes;

    public KeysConfiguration(Player.PlayerID playerId)
    {
        PlayerId = playerId;
        KeyCodes = new Dictionary<KeyCode, Action>();
    }

    public void UpdateKey(KeyCode key, Action action)
    {
        RemoveKey(key);
        RemoveAction(action);
        KeyCodes.Add(key, action);
    }

    public void ActionKey(KeyCode key)
    {
        if (!KeyCodes.ContainsKey(key)) return;
        switch (KeyCodes[key])
        {
            case Action.None:
                return;
            case Action.Fired:
                PlayersController.Fired(PlayerId);
                break;
            case Action.MoveUp:
                PlayersController.MoveShip(PlayerId, new Vector2(0, 1));
                break;
            case Action.MoveDown:
                PlayersController.MoveShip(PlayerId, new Vector2(0, -1));
                break;
            case Action.MoveRight:
                PlayersController.MoveShip(PlayerId, new Vector2(1, 0));
                break;
            case Action.MoveLeft:
                PlayersController.MoveShip(PlayerId, new Vector2(-1, 0));
                break;
            default:
                return;
        }
    }

    private void RemoveKey(KeyCode keyCode)
    {
        if (KeyCodes.ContainsKey(keyCode))
            KeyCodes.Remove(keyCode);
    }

    private void RemoveAction(Action action)
    {
        var keyAux = KeyCode.None;
        foreach (var keyValuePair in KeyCodes)
        {
            if (keyValuePair.Value != action) continue;
            keyAux = keyValuePair.Key;
        }

        RemoveKey(keyAux);
    }
}