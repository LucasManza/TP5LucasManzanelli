using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace controller
{
    public static class Store
    {
        public static Dictionary<Player.PlayerID, Player> Players = new Dictionary<Player.PlayerID, Player>();
        public static List<KeysConfiguration> KeysConfigurationsMoves = new List<KeysConfiguration>();
        public static List<KeysConfiguration> KeysConfigurationsFired = new List<KeysConfiguration>();
        private static readonly List<Collisionable> Meteorites = new List<Collisionable>();
        private static readonly List<Collisionable> Bullets = new List<Collisionable>();
        private static readonly List<Collisionable> PowerUps = new List<Collisionable>();
        public static World World;


        public static void SaveMeteorite(Meteorite meteorite)
        {
            Meteorites.Add(meteorite);
        }

        public static void SaveBullet(Bullet bullet)
        {
            Bullets.Add(bullet);
        }

        public static void SavePowerUp(Collisionable collisionable)
        {
            if (collisionable.GetType() == Type.Health || collisionable.GetType() == Type.Weapon)
            {
                Debug.Log("POWER UP SAVE!");
                PowerUps.Add(collisionable);
            }
        }

        public static List<Collisionable> GetMeteorites()
        {
            return Meteorites;
        }

        public static List<Collisionable> GetBullets()
        {
            return Bullets;
        }

        public static List<Collisionable> GetPowerUps()
        {
            return PowerUps;
        }

        public static List<Collisionable> GetAllShips()
        {
            var result = new List<Collisionable>();
            foreach (var p in Store.Players)
            {
                var ship = p.Value.GetShip();
                if (ship != null)
                    result.Add(ship);
            }

            return result;
        }

        public static void ShowCollisionables()
        {
            Debug.Log("Meteorites Count: " + Meteorites.Count);
            Debug.Log("Bullets Count: " + Bullets.Count);
            Debug.Log("Ships Count: " + GetAllShips().Count);
//            GetBullets().ForEach(b =>
//            {
//                var b1 = (Bullet) b;
//                Debug.Log("STORE | Bullet" + b1.GetType() + " | PlayerID : " + b1.PlayerId);
//            });
//
//            GetAllShips().ForEach(s =>
//            {
//                var s1 = (Ship) s;
//                Debug.Log("STORE | Ship" + s1.GetType());
//            });
//
//            GetMeteorites().ForEach(m =>
//            {
//                var m1 = (Meteorite) m;
//                Debug.Log("STORE | " + m1.GetType() + " | PlayerID : " + m1.GetDirection());
//            });
        }
    }
}