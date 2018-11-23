using UnityEngine;

namespace Weapons
{
    public class BigWeapon : Weapon
    {
        public override Bullet FiredWeapon(Player.PlayerID id, float charge, Vector2 position, Vector2 direction)
        {
            var gObject = Instantiate(BulletGameObject, gameObject.transform.position, Quaternion.identity);
            var bulletScript = gObject.GetComponent<Bullet>();
            bulletScript.PlayerId = id;
            bulletScript.Damage = TopDamage;
            bulletScript.Position = position;
            bulletScript.SetDirection(direction);
            return bulletScript;
        }
    }
}