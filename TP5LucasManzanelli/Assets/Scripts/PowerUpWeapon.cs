using UnityEngine;

public class PowerUpWeapon : Collisionable
{
    public Weapon Weapon;

    private void Awake()
    {
        Type = Type.Weapon;
    }

    private void OnTriggerEnter(Collider other)
    {
        var collisionable = other.GetComponent<Collisionable>();
        if (collisionable == null || collisionable.GetType() != Type.Ship) return;

        ((Ship) collisionable).UpdateWeapon(Weapon);
        ChangeStatus(Status.Destroy);
    }

    public override void CollisionWith(Collisionable collision)
    {
    }
}