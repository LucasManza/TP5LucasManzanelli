using System.Collections.Generic;
using UnityEngine;

namespace controller
{
    public static class GameController
    {
        public static void GameOver()
        {
            Store.GetMeteorites().ForEach(m => { m.InmediateDestroy(); });
            Store.GetBullets().ForEach(b => { b.InmediateDestroy(); });
            Store.GetMeteorites().Clear();
            Store.GetBullets().Clear();
        }

        public static bool CheckWorldLimits(Vector2 position)
        {
            return Store.World != null && !Store.World.IsColliding(position);
        }

        public static void CheckDestroyItems()
        {
            RemoveDestroyItems(Store.GetMeteorites());
            RemoveDestroyItems(Store.GetBullets());
        }

        private static void RemoveDestroyItems(List<Collisionable> collisionables)
        {
            collisionables.RemoveAll(c => c.CurrentStatus == Collisionable.Status.Destroy);
        }
    }
}