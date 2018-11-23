using System;
using UnityEngine;

public class Vector2
{
    private readonly float _x;
    private readonly float _y;

    public Vector2(float x, float y)
    {
        _x = x;
        _y = y;
    }

    public Vector2 Add(Vector2 vector2)
    {
        return new Vector2(_x + vector2._x, _y + vector2._y);
    }

    public Vector2 Subtract(Vector2 vector2)
    {
        return new Vector2(_x - vector2._x, _y - vector2._y);
    }

    public Vector2 Multiply(float magnitude)
    {
        return new Vector2(_x * magnitude, _y * magnitude);
    }

    public Vector2 Rotate(float angle)
    {
        return new Vector2((float) (_x * Math.Cos(angle) - _y * Math.Sin(angle)),
            (float) (_x * Math.Sin(angle) + _y * Math.Cos(angle)));
    }

    public float Module()
    {
        return (float) Math.Pow(Math.Pow(_x, 2) + Math.Pow(_y, 2), 0.5);
    }

    public Vector2 Normalize()
    {
        var module = Module();
        return new Vector2(_x / module, _y / module);
    }

    public float Angle()
    {
        return (float) (Math.Atan2(_y, _x) - Math.Atan2(0, 1));
    }

    public bool Contains(Vector2 vectorPoint, float ownRadius)
    {
        return (Subtract(vectorPoint).Module()) <= ownRadius;
    }

    public float X
    {
        get { return _x; }
    }

    public float Y
    {
        get { return _y; }
    }

    public string ToStr()
    {
        return "(" + _x + " ," + _y + ")";
    }
}