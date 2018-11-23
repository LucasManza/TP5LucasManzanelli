using System;
using System.Collections.Generic;
using controller;
using UnityEngine;
using UnityEngine.UI;

public class ConfigurationManagement : MonoBehaviour
{
//    public List<Text> P1Moves;
//    public List<Text> P2Moves;
//    public Text P1Fired;
//    public Text P2Fired;
    public ConfigInputMap.ConfigInput[] P1Input;
    public ConfigInputMap.ConfigInput[] P2Input;
    public ConfigInputMap.ConfigInput P1FiredInput;
    public ConfigInputMap.ConfigInput P2FiredInput;


    private void Start()
    {
        AssignConfiguration();
    }

    public void UpdateKeysConfiguration()
    {
        var keysConfigFired = Store.KeysConfigurationsFired;
        var keysConfigMove = Store.KeysConfigurationsMoves;

        if (keysConfigFired == null || keysConfigMove == null) return;

        keysConfigMove.ForEach(k =>
        {
            switch (k.PlayerId)
            {
                case Player.PlayerID.None:
                    break;
                case Player.PlayerID.Player1:
                    SetKeys(k, P1Input);
                    break;
                case Player.PlayerID.Player2:
                    SetKeys(k, P2Input);
                    break;
                case Player.PlayerID.Player3:
                    break;
                case Player.PlayerID.Player4:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        });

        keysConfigFired.ForEach(k =>
        {
            switch (k.PlayerId)
            {
                case Player.PlayerID.None:
                    break;
                case Player.PlayerID.Player1:
                    SetKeys(k, P1FiredInput);
                    break;
                case Player.PlayerID.Player2:
                    SetKeys(k, P2FiredInput);
                    break;
                case Player.PlayerID.Player3:
                    break;
                case Player.PlayerID.Player4:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        });
    }

    private void AssignConfiguration()
    {
        var kConfigMoves = Store.KeysConfigurationsMoves;
        var kConfigFired = Store.KeysConfigurationsFired;

        if (kConfigMoves == null || kConfigFired == null) return;
        kConfigMoves.ForEach(k =>
        {
            switch (k.PlayerId)
            {
                case Player.PlayerID.None:
                    break;
                case Player.PlayerID.Player1:
                    AssignKeyCode(k, P1Input);
                    break;
                case Player.PlayerID.Player2:
                    AssignKeyCode(k, P2Input);
                    break;
                case Player.PlayerID.Player3:
                    break;
                case Player.PlayerID.Player4:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        });

        kConfigFired.ForEach(k =>
        {
            switch (k.PlayerId)
            {
                case Player.PlayerID.None:
                    break;
                case Player.PlayerID.Player1:
                    AssignKeyCode(k, P1FiredInput);
                    break;
                case Player.PlayerID.Player2:
                    AssignKeyCode(k, P2FiredInput);
                    break;
                case Player.PlayerID.Player3:
                    break;
                case Player.PlayerID.Player4:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        });
    }

    private void AssignKeyCode(KeysConfiguration k, ConfigInputMap.ConfigInput[] configInputs)
    {
        foreach (var c in configInputs)
        {
            foreach (var keyValuePair in k.KeyCodes)
            {
                if (keyValuePair.Value != c.Action) continue;
                c.InputField.text = keyValuePair.Key.ToString();
            }
        }
    }

    private void AssignKeyCode(KeysConfiguration k, ConfigInputMap.ConfigInput configInput)
    {
        foreach (var keyValuePair in k.KeyCodes)
        {
            if (keyValuePair.Value != configInput.Action) continue;
            configInput.InputField.text = keyValuePair.Key.ToString();
        }
    }

    private void SetKeys(KeysConfiguration keysConfiguration, ConfigInputMap.ConfigInput[] configInputs)
    {
        foreach (var c in configInputs)
        {
            var txt = c.InputField.text;
            Debug.Log("New Key Value : " + txt);
            keysConfiguration.UpdateKey(txt.Length < 2 ? ParseToKeyCode(txt[0]) : ParseToKeyCode(txt), c.Action);
        }
    }

    private void SetKeys(KeysConfiguration keysConfiguration, ConfigInputMap.ConfigInput configInputs)
    {
        var txt = configInputs.InputField.text;
        keysConfiguration.UpdateKey(txt.Length < 2 ? ParseToKeyCode(txt[0]) : ParseToKeyCode(txt), configInputs.Action);
    }

    private KeyCode ParseToKeyCode(char c)
    {
        var c1 = c.ToString().ToUpper();
        return (KeyCode) Enum.Parse(typeof(KeyCode), c1);
    }

    private KeyCode ParseToKeyCode(string s)
    {
        int result;
        switch (s.ToUpper())
        {
            case "SPACE":
                result = 32;
                break;
            case "UP":
                result = 273;
                break;
            case "DOWN":
                result = 274;
                break;
            case "RIGHT":
                result = 275;
                break;
            case "LEFT":
                result = 276;
                break;
            default:
                result = 0;
                break;
        }

        return (KeyCode) result;
    }
}