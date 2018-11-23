using UnityEngine;

public class World : MonoBehaviour
{
    public Camera Camera;
    private Vector2 _superiorRightLimit;
    private Vector2 _inferiorLeftLimit;


    private void Awake()
    {
//        var z = Camera.main.transform.position.z;
        if (Camera == null)
            Camera = Camera.current;
        var z = Camera.transform.position.z;

        _superiorRightLimit = new Vector2(z, z / 2);
        _inferiorLeftLimit = new Vector2(-z, -z / 2);

//        _upCollider = GenerateCollider(new Vector3(0, z / 2, 0), new Vector3(z / 2, 1, 1));
//        _downCollider = GenerateCollider(new Vector3(0, -z / 2, 0), new Vector3(z / 2, 1, 1));
//        _leftCollider = GenerateCollider(new Vector3(z, 0, 0), new Vector3(1, z / 2, 1));
//        _rightCollider = GenerateCollider(new Vector3(-z, 0, 0), new Vector3(1, z / 2, 1));
    }


    private void OnTriggerEnter(Collider other)
    {
        var collisionable = other.gameObject.GetComponent<Collisionable>();
        
        if(collisionable == null || collisionable.GetType() != Type.Meteorite || collisionable.GetType() != Type.Ship) return;
        
        collisionable.SetDirection(collisionable.GetDirection().Rotate(180));
    }

    public bool IsColliding(Vector2 position)
    {
        if (position.X >= _superiorRightLimit.X || position.X <= _inferiorLeftLimit.X)
            return false;
        if (position.Y >= _superiorRightLimit.Y || position.Y <= _inferiorLeftLimit.Y)
            return false;
        return true;
    }

    public Vector2 RandomPosition()
    {
        var x = Random.Range(_inferiorLeftLimit.X, _superiorRightLimit.X);
        var y = Random.Range(_inferiorLeftLimit.Y, _superiorRightLimit.Y);
        return new Vector2(x, y);
    }

    public Vector2 RandomPositionLimits()
    {
        if (Random.Range(0f, 1f) <= 0.5f)
        {
            return new Vector2(Random.Range(_inferiorLeftLimit.X, _superiorRightLimit.X),
                _superiorRightLimit.Y * (Random.Range(0, 2) * 2 - 1));
        }

        return new Vector2(_superiorRightLimit.X * (Random.Range(0, 2) * 2 - 1),
            Random.Range(_inferiorLeftLimit.Y, _superiorRightLimit.Y));
    }

    private BoxCollider GenerateCollider(Vector3 position, Vector3 scale)
    {
        var boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.center = position;
        boxCollider.gameObject.transform.localScale = position;
        return boxCollider;
    }
}