using System;
using controller;
using UnityEngine;

public class Bullet : Collisionable
{
    public Player.PlayerID PlayerId;
    public float Speed;
    private float _timeDuration;


    private void Start()
    {
        _timeDuration = _timeDuration <= 0 ? 15f : _timeDuration;
        Speed = Speed > 0 ? Speed : 0f;
        Type = Type.Bullet;
    }

    private void Update()
    {
        CheckExplotion();
        DestroyGameObject();
        if (_timeDuration >= 0)
            _timeDuration -= Time.deltaTime;

        else
        {
            ChangeStatus(Status.Exploted);
            InitTimerExplotion();
        }

        Move(Direction);
    }

    protected override void CollisionWith(Collider other)
    {
        var collision = other.gameObject.GetComponent<Collisionable>();
        if (collision == null || collision.GetType() == Type.None || CurrentStatus != Status.Normal) return;

        if (collision.GetType() == Type.Bullet && ((Bullet) collision).PlayerId == PlayerId) return;
        collision.DecreaseLife(Damage);
        if (collision.CurrentStatus == Status.Destroy || collision.GetCurrentLife() <= 0)
            PlayersController.IncrementScore(PlayerId, collision.Score);

        ChangeStatus(Status.Exploted);
    }

    public override void Move(Vector2 direction)
    {
        if (CurrentStatus != Status.Normal) return;
        Position = Movement.Move(Position, direction, Speed, true);
    }
}