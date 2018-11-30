using UnityEngine;
using Weapons;

public class PowerUpWeapon : Collisionable
{
    public GameObject Weapon;

    private void Awake()
    {
        Type = Type.Weapon;
        if (Weapon.GetComponent<Weapon>() == null)
            Weapon.AddComponent<DefaultWeapon>();
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
            ((Ship) collisionable).UpdateWeapon(Weapon);
        }

        ChangeStatus(Status.Destroy);
    }
}