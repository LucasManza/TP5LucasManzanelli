using UnityEngine;

public class World : MonoBehaviour
{
    public Camera Camera;

    private Vector2 _superiorRightLimit;
    private Vector2 _inferiorLeftLimit;
    private LimitCollider _upCollider;
    private LimitCollider _downCollider;
    private LimitCollider _leftCollider;
    private LimitCollider _rightCollider;


    private void Awake()
    {
        if (Camera == null)
            Camera = Camera.current;
        var z = Camera.gameObject.transform.position.z;

        _superiorRightLimit = new Vector2(z, z / 2);
        _inferiorLeftLimit = new Vector2(-z, -z / 2);

        var extraMargin = 4;
        _upCollider = GenerateLimit(LimitCollider.LimitId.Up, new Vector3(0, z / 2 - extraMargin, 0),
            new Vector3(z / 2, 0.5f, 1));
        _downCollider = GenerateLimit(LimitCollider.LimitId.Down, new Vector3(0, -z / 2 + extraMargin, 0),
            new Vector3(z / 2, 0.5f, 1));
        _rightCollider = GenerateLimit(LimitCollider.LimitId.Right, new Vector3(z - extraMargin, 0, 0),
            new Vector3(0.5f, z / 2, 1));
        _leftCollider = GenerateLimit(LimitCollider.LimitId.Left, new Vector3(-z + extraMargin, 0, 0),
            new Vector3(0.5f, z / 2, 1));
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

    private LimitCollider GenerateLimit(LimitCollider.LimitId id, Vector3 position, Vector3 scale)
    {
        var gObject = Instantiate(new GameObject(), position, Quaternion.identity);
        gObject.transform.SetParent(gameObject.transform);
        gObject.transform.localScale = scale;
        var l = gObject.AddComponent<LimitCollider>();
        l.Id = id;
        return l;
    }
}