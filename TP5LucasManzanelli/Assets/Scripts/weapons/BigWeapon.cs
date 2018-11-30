using UnityEngine;

namespace Weapons
{
    public class BigWeapon : Weapon
    {
        private void Update()
        {
            if (TopAmmo <= 0) Destroy(gameObject);
        }

        public override Bullet FiredWeapon(Player.PlayerID id, float charge, Vector2 position, Vector2 direction)
        {
            var gObject = Instantiate(BulletGameObject, gameObject.transform.position, Quaternion.identity);
            var bulletScript = gObject.GetComponent<Bullet>();
            bulletScript.PlayerId = id;
            bulletScript.Position = position;
            bulletScript.SetDirection(direction);
            TopAmmo -= 1;
            return bulletScript;
        }
    }
}