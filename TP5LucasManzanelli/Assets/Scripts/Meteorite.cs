using UnityEngine;

public class Meteorite : Collisionable
{
    public float Speed;
    private float _currentLife;

    private void Start()
    {
        Type = Type.Meteorite;
        Speed = Speed > 0 ? Speed : 5f;
        _currentLife = Life > 0 ? Life : 1f;
    }

    private void Update()
    {
        CheckExplotion();
        DestroyGameObject();
        InitTimerExplotion();
        Move(Direction);
    }

    private void OnTriggerEnter(Collider other)
    {
        var collision = other.gameObject.GetComponent<Collisionable>();
        if (!collision || collision.GetType() == Type.None || collision.GetType() == Type.Bullet ||
            CurrentStatus != Status.Normal)
            return;
        collision.DecreaseLife(Damage);
        if (collision.GetType() == Type.Ship)
            ChangeStatus(Status.Exploted);
    }

    public override void Move(Vector2 direction)
    {
        if (CurrentStatus != Status.Normal) return;
        Position = Movement.Move(Position, direction, Speed, false);
    }
}