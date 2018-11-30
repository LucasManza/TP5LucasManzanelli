using UnityEngine;

public class LimitCollider : MonoBehaviour
{
    public enum LimitId
    {
        Left,
        Right,
        Up,
        Down,
        None
    }

    public LimitId Id = LimitId.None;
    private Vector3 _position;

    private void Start()
    {
        gameObject.AddComponent<BoxCollider>();
        gameObject.GetComponent<BoxCollider>().isTrigger = true;
    }

    private void OnTriggerExit(Collider other)
    {
        var collisionable = other.gameObject.GetComponent<Collisionable>();

        if (collisionable == null) return;
        if (collisionable.GetType() == Type.Ship)
            TeleportShip(collisionable);
        else
            collisionable.ChangeStatus(Collisionable.Status.Destroy);
    }

    private void TeleportShip(Collisionable ship)
    {
        if (Id == LimitId.None)
            ship.Position = new Vector2(0, 0);

        var pos = gameObject.transform.position;

        if (Id == LimitId.Left || Id == LimitId.Right)
            ship.Position = ship.Position.Add(new Vector2(-pos.x, 0));
        else
            ship.Position = ship.Position.Add(new Vector2(0, -pos.y));
    }
}