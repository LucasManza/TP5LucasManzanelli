using System.Collections.Generic;
using UnityEngine;

namespace controller
{
    public class CollisionEngine
    {
        public void CheckCollisions(List<Collisionable> collisionables1, List<Collisionable> collisionables2)
        {
//            Debug.Log("List 1 : " + collisionables1.Count + " | List 2 : " + collisionables2.Count);

            if (collisionables1.Count <= 0 || collisionables2.Count <= 0)
            {
                return;
            }

            CheckCollisions(collisionables1, collisionables2, collisionables1.Count < collisionables2.Count);
        }

        private void CheckCollisions(List<Collisionable> fst, List<Collisionable> snd, bool firstSmaller)
        {
            var length = firstSmaller ? fst.Count : snd.Count;
            for (var i = 0; i < length; i++)
            {
                if (!Intersects(fst[i], snd[i])) continue;
                Debug.LogWarning("Intersects: " + fst[i].GetType() + " | " + snd[i].GetType());
                fst[i].CollisionWith(snd[i]);
//                snd[i].CollisionWith(fst[i]);
            }
        }

        private bool Intersects(Collisionable collisionableA, Collisionable collisionableB)
        {
//            if (collisionableB.Position.X > collisionableA.Position.X * collisionableA.ImpactRadius ||
//                collisionableB.Position.X < collisionableA.Position.X * -collisionableA.ImpactRadius)
//                return false;
//            return !(collisionableB.Position.Y > collisionableA.Position.Y * collisionableA.ImpactRadius) &&
//                   !(collisionableB.Position.Y < collisionableA.Position.Y * -collisionableA.ImpactRadius);
            
            return collisionableA.Position.Contains(collisionableB.Position, collisionableA.ImpactRadius) ||
                   collisionableB.Position.Contains(collisionableA.Position, collisionableB.ImpactRadius);
        }
    }
}