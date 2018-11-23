using UnityEngine;

namespace view
{
    public class CollisionableView : MonoBehaviour
    {
        public SpriteRenderer Image;
        public SpriteRenderer ExplosionImage;
        public Color Color;
        private Collisionable _collisionable;
        private Sprite _sprite;

        private void Start()
        {
            _collisionable = gameObject.GetComponent<Collisionable>()
                ? gameObject.GetComponent<Collisionable>()
                : null;
            ChangeViewSize();
            ChangeView();
//            Image.color = Color.Lerp(Color.white, Color.red,
//                _collisionable.GetCurrentLife() / _collisionable.Life);
        }


        private void Update()
        {
            if (_collisionable == null)
                return;
            ChangeViewSize();
            MoveToPosition(_collisionable.Position);
            ChangeView();
            ChangeRotation(_collisionable.GetDirection());
            Image.color = Color.Lerp(Color.red, Color.white, _collisionable.GetCurrentLife() / _collisionable.Life);
        }

        private void ChangeView()
        {
            switch (_collisionable.CurrentStatus)
            {
                case Collisionable.Status.Normal:
                    Image.enabled = (true);
                    ExplosionImage.enabled = (false);
                    break;
                case Collisionable.Status.Exploted:
                    Image.enabled = (false);
                    ExplosionImage.enabled = (true);
                    break;
                case Collisionable.Status.Destroy:
                    Image.enabled = (false);
                    ExplosionImage.enabled = (false);
                    break;
                default:
                    Image.enabled = (true);
                    ExplosionImage.enabled = (false);
                    break;
            }
        }

        private void ChangeViewSize()
        {
            if (_collisionable != null)
                gameObject.transform.localScale =
                    new Vector3(_collisionable.ImpactRadius, _collisionable.ImpactRadius, 1f);
        }

        private void MoveToPosition(Vector2 position)
        {
            gameObject.transform.position = new Vector3(position.X, position.Y, 0f);
        }

        private void ChangeRotation(Vector2 vector2)
        {
            var rotation = new Vector3(0, 0, 0);

            if (vector2.X > 0f || vector2.X < 0f)
            {
                rotation.z = vector2.Normalize().X * -90;
            }
            else if (vector2.Y > 0f)
            {
                rotation.z = vector2.Normalize().Y * 0;
            }
            else if (vector2.Y < 0f)
                rotation.z = vector2.Normalize().Y * 180;


            gameObject.transform.rotation = Quaternion.Euler(rotation);
        }
    }
}