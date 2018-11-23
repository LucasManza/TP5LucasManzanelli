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
        Move();
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

    public void Move()
    {
        if (CurrentStatus != Status.Normal) return;

//        var probability = Random.Range(0f, 1f);
//        Position = probability > 0.1f
//            ? Movement.Move(Position, Direction, Speed, false)
//            : Movement.RandomMove(Position, Speed);
        Position = Movement.Move(Position, Direction, Speed, false);
    }

    public override void CollisionWith(Collisionable collision)
    {
    }
}