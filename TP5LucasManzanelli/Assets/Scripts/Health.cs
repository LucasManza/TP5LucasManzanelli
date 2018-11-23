using UnityEngine;

public class Health : Collisionable
{
    private void Awake()
    {
        Type = Type.Health;
    }

    private void OnTriggerEnter(Collider other)
    {
        var collisionable = other.GetComponent<Collisionable>();
        if (collisionable == null || collisionable.GetType() != Type.Ship) return;

        ((Ship) collisionable).RestoreLife();
        ChangeStatus(Status.Destroy);
    }

    public override void CollisionWith(Collisionable collision)
    {
    }
}