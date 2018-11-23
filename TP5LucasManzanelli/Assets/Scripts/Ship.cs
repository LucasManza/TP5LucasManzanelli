using controller;
using UnityEngine;

public class Ship : Collisionable
{
    public float Speed;
    public Weapon Weapon;

    private void Start()
    {
        Type = Type.Ship;
        Speed = Speed < 1 ? 100 : Speed;
        WeaponPosition();
    }

    private void Update()
    {
        CheckExplotion();
    }

    public void Move(Vector2 direction)
    {
        if (CurrentStatus != Status.Normal) return;
        Position = Movement.Move(Position, direction, Speed, false);
        Direction = direction.Normalize();
//        Weapon.gameObject.transform.position = gameObject.transform.position;
        WeaponPosition();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("SHIP ON COLLISION ENTER!");
        var collision = other.gameObject.GetComponent<Collisionable>();
        if (!collision || collision.GetType() == Type.None)
            return;
        collision.DecreaseLife(Damage);
    }

    public void FiredWeapon(Player.PlayerID id)
    {
        if (Weapon == null)
        {
            Debug.Log("None Weapon!");
            return;
        }

        var weaponPosition =
            new Vector2(Weapon.gameObject.transform.position.x, Weapon.gameObject.transform.position.y);
        Store.SaveBullet(Weapon.FiredWeapon(id, 1, weaponPosition, Direction));
    }

    public void UpdateWeapon(Weapon weapon)
    {
        Weapon = weapon;
    }

    public void IncrementLife(float amount)
    {
        var result = CurrentLife + amount;
        if (result >= Life)
            CurrentLife = Life;
        else
            CurrentLife = result;
        Debug.Log("Increase Life");
    }

    public void RestoreLife()
    {
        IncrementLife(Life);
        ChangeStatus(Status.Normal);
    }

    public override void CollisionWith(Collisionable collision)
    {
//        if (!collision || collision.GetType() == Type.None)
//            return;
//        collision.DecreaseLife(Damage);
    }

    private void WeaponPosition()
    {
        if (Weapon == null || Weapon.BulletGameObject == null ||
            Weapon.BulletGameObject.GetComponent<Collisionable>()) return;
//        var distance = ImpactRadius + Weapon.BulletGameObject.GetComponent<Collisionable>().ImpactRadius;
//        var direction = Position.Add(Direction.Multiply(distance*5));
//        Weapon.gameObject.transform.position = new Vector3(direction.X, direction.Y, 0);

        var direction = Position.Add(Direction.Multiply(5));
        Weapon.gameObject.transform.position = new Vector3(direction.X, direction.Y, 0);

        Debug.Log("Ship Position: " + gameObject.transform.position);
        Debug.Log("Weapon Position: " + Weapon.gameObject.transform.position);
    }
}