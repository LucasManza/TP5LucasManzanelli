using UnityEngine;


public abstract class Collisionable : MonoBehaviour
{
    public Vector2 Position;
    public float Life;
    public float ImpactRadius;
    public float Score;
    public float Damage;
    protected Type Type;
    protected Vector2 Direction;
    protected float CurrentLife;
    protected float ExplotedTimer;
    protected internal Status CurrentStatus;


    public enum Status
    {
        Normal,
        Exploted,
        Destroy,
    }

    private void Awake()
    {
        Direction = Direction ?? new Vector2(0, 1);
        Position = Position ?? new Vector2(0, 0);
        Damage = Damage > 0 ? Damage : 0f;
        Life = Life > 0 ? Life : 1f;
        CurrentLife = Life;
        ExplotedTimer = 1f;
        CurrentStatus = Status.Normal;

        var collider = gameObject.GetComponent<CapsuleCollider>() == null
            ? gameObject.AddComponent<CapsuleCollider>()
            : gameObject.GetComponent<CapsuleCollider>();
        collider.radius = ImpactRadius;
        collider.isTrigger = true;


        var rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
    }

    public abstract void Move(Vector2 direction);

    public void NextStatus()
    {
        switch (CurrentStatus)
        {
            case Status.Normal:
                Damage = 0;
                CurrentStatus = Status.Exploted;
                break;
            case Status.Exploted:
                Damage = 0;
                CurrentStatus = Status.Destroy;
                break;
            case Status.Destroy:
                Damage = 0;
                CurrentStatus = Status.Destroy;
                break;
            default:
                CurrentStatus = Status.Normal;
                break;
        }
    }

    public void ChangeStatus(Status status)
    {
        switch (status)
        {
            case Status.Normal:
                CurrentStatus = Status.Normal;
                break;
            case Status.Exploted:
                Damage = 0;
                CurrentStatus = Status.Exploted;
                break;
            case Status.Destroy:
                Damage = 0;
                CurrentStatus = Status.Destroy;
                break;
            default:
                CurrentStatus = Status.Normal;
                break;
        }
    }

    protected void InitTimerExplotion()
    {
        if (CurrentStatus != Status.Exploted) return;
//        Debug.Log("EXPLOSION TIMER: " + ExplotedTimer);
        ExplotedTimer -= Time.deltaTime;
        if (ExplotedTimer <= 0f)
            ChangeStatus(Status.Destroy);
    }

    public void DestroyGameObject()
    {
        if (CurrentStatus != Status.Destroy) return;
        Destroy(gameObject);
    }

    public void InmediateDestroy()
    {
        if (gameObject != null)
        {
            Destroy(gameObject);
        }
    }

    public void DecreaseLife(float amount)
    {
        var result = CurrentLife - amount;
        if (result <= 0)
        {
            CurrentLife = 0;
            ChangeStatus(Status.Exploted);
        }

        CurrentLife = result;
        Debug.Log("Life ; " + CurrentLife + " Type : " + Type);
    }

    public void CheckExplotion()
    {
        if (CurrentStatus == Status.Normal && CurrentLife <= 0)
        {
            ChangeStatus(Status.Exploted);
        }
    }

    public void SetDirection(Vector2 direction)
    {
        Direction = direction;
    }

    //GETTERS     

    public Vector2 GetDirection()
    {
        return Direction ?? new Vector2(0, 0);
    }

    public float GetCurrentLife()
    {
        return CurrentLife;
    }

    public Type GetType()
    {
        return Type;
    }
}