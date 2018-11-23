using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace controller
{
    public static class PlayersController
    {
        public static void IncrementScore(Player.PlayerID playerId, float amount)
        {
            if (!Store.Players.ContainsKey(playerId)) return;
            Store.Players[playerId].IncreaseScore(amount);
        }

        public static void DecrementScore(Player.PlayerID playerId, float amount)
        {
            if (!Store.Players.ContainsKey(playerId)) return;
            Store.Players[playerId].DecreaseScore(amount);
        }

        public static void MoveShip(Player.PlayerID playerId, Vector2 direction)
        {
            if (!Store.Players.ContainsKey(playerId))
                return;
            var ship = Store.Players[playerId].GetShip();
            if (ship == null || GameController.CheckWorldLimits(ship.Position.Add(direction).Multiply(ship.Speed)))
                return;

            ship.Move(direction);
        }

        public static void Fired(Player.PlayerID playerId)
        {
            if (!Store.Players.ContainsKey(playerId))
                return;
            var ship = Store.Players[playerId].GetShip();
            if (ship == null)
                return;

            ship.FiredWeapon(playerId);
        }

        public static bool SetKeyCode(Player.PlayerID playerId, KeyCode keyCode, KeysConfiguration.Action action)
        {
            if (ValidateKey(playerId, keyCode)) return false;

            foreach (var keyConfig in Store.KeysConfigurationsMoves)
            {
                if (keyConfig.PlayerId != playerId) continue;
                keyConfig.UpdateKey(keyCode, action);
                return true;
            }

            return false;
        }

        public static List<Player> GetPlayersByScore()
        {
            var result = Store.Players.Values.ToList();
            result.Sort((x, y) => x.CurrentScore.CompareTo(y.CurrentScore));
            result.Reverse();
            return result;
        }

        private static bool ValidateKey(Player.PlayerID playerId, KeyCode keyCode)
        {
            foreach (var keyConfig in Store.KeysConfigurationsMoves)
            {
                if (keyConfig.PlayerId == playerId)
                    continue;
                if (keyConfig.KeyCodes.ContainsKey(keyCode))
                {
                    return false;
                }
            }

            return true;
        }
    }
}