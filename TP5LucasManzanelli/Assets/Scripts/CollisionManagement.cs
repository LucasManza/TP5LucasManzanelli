using System.Collections.Generic;
using controller;
using UnityEngine;

public class CollisionManagement : MonoBehaviour
{
    private readonly CollisionEngine _collisionEngine = new CollisionEngine();

    private void FixedUpdate()
    {
        Collisions();
    }

    public void Collisions()
    {
//            Store.ShowCollisionables();
        _collisionEngine.CheckCollisions(Store.GetBullets(),
            Store.GetAllShips());
        _collisionEngine.CheckCollisions(Store.GetBullets(),
            Store.GetMeteorites());
        _collisionEngine.CheckCollisions(Store.GetMeteorites(),
            Store.GetAllShips());
    }

    public List<Collisionable> CheckDestroys(List<Collisionable> list)
    {
        var result = new List<Collisionable>();
        list.ForEach(l =>
        {
            if (l.CurrentStatus != Collisionable.Status.Destroy)
                result.Add(l);
        });
        return result;
    }
}