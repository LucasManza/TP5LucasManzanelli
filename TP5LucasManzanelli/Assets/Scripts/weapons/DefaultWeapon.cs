using UnityEngine;

namespace Weapons
{
    public class DefaultWeapon : Weapon
    {
       

        public override Bullet FiredWeapon(Player.PlayerID id, float charge, Vector2 position, Vector2 direction)
        {
            if (Upgrade != null)
            {
                return Upgrade.FiredWeapon(id, charge, position, direction);
            }

            var gObject = Instantiate(BulletGameObject, gameObject.transform.position, Quaternion.identity);
            gObject.transform.SetParent(gameObject.transform);
            var bulletScript = gObject.GetComponent<Bullet>();
            bulletScript.PlayerId = id;
            bulletScript.Position = position;
            bulletScript.SetDirection(direction);
            return bulletScript;
        }
    }
}