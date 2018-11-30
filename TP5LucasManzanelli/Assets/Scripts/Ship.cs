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
        WeaponPosition(Weapon);
    }

    private void Update()
    {
        CheckExplotion();
    }

    public override void Move(Vector2 direction)
    {
        if (CurrentStatus != Status.Normal) return;
        Position = Movement.Move(Position, direction, Speed, false);
        Direction = direction.Normalize();
        WeaponPosition(Weapon);
    }

    protected override void CollisionWith(Collider other)
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

    public void UpdateWeapon(GameObject weapon)
    {
        if (weapon == null || this.Weapon == null)
            return;


        var pos = gameObject.transform.position;
        pos = pos + new Vector3(0, 4, 0);
        var w = Instantiate(weapon, pos, Quaternion.identity);
//        w.gameObject.transform.SetParent(gameObject.transform);
        this.Weapon.UpgradeWeapon(w.GetComponent<Weapon>());
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

    private void WeaponPosition(Weapon weapon)
    {
        if (weapon == null || weapon.BulletGameObject == null ||
            weapon.BulletGameObject.GetComponent<Collisionable>()) return;
        var direction = Position.Add(Direction.Multiply(5));
        weapon.gameObject.transform.position = new Vector3(direction.X, direction.Y, 0);
        WeaponPosition(weapon.Upgrade);
    }
}