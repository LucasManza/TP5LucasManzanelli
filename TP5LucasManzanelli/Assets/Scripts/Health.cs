using UnityEngine;

public class Health : Collisionable
{
    private void Awake()
    {
        Type = Type.Health;
    }

    public override void Move(Vector2 direction)
    {
    }

    protected override void CollisionWith(Collider other)
    {
        var collisionable = other.GetComponent<Collisionable>();
        if (collisionable == null) return;

        if (collisionable.GetType() == Type.Ship)
        {
            ((Ship) collisionable).RestoreLife();
        }

        ChangeStatus(Status.Destroy);
    }
}