using UnityEngine;


public abstract class Weapon : MonoBehaviour
{
    public GameObject BulletGameObject;
    public Weapon Upgrade;
    public float TopAmmo;


    public abstract Bullet FiredWeapon(Player.PlayerID id, float charge, Vector2 position, Vector2 direction);

    public void UpgradeWeapon(Weapon weapon)
    {
        Upgrade = weapon;
    }
}