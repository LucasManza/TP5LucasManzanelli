using UnityEngine;


public static class Movement
{
    public static Vector2 Move(Vector2 currentPosition, Vector2 direction, float speed, bool ignoreWorldCollision)
    {
        var move = currentPosition.Add(direction.Normalize().Multiply(speed * Time.deltaTime));
//            return GameController.World.IsColliding(move) ? currentPosition : move;
        return move;
    }

    public static Vector2 RandomMove(Vector2 currentPosition, float speed)
    {
        return Move(currentPosition, RandomDirection(), speed, false);
    }

    public static Vector2 RandomDirection()
    {
        var x = Random.Range(-1, 1);
        var y = x == 0
            ? (Random.Range(0, 2)) * 2 - 1
            : Random.Range(-1, 1);
        return new Vector2(x, y);
    }
}