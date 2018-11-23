public class Player
{
    public enum PlayerID
    {
        None,
        Player1,
        Player2,
        Player3,
        Player4,
    }

    public PlayerID ID;
    public string NickName;
    public float CurrentScore;
    private Ship _ship;

    public Player(PlayerID id, string nickName, Ship ship)
    {
        ID = id;
        NickName = nickName;
        CurrentScore = 0f;
        _ship = ship;
    }

    public Player(string nickName, Ship ship)
    {
        ID = PlayerID.None;
        NickName = nickName;
        CurrentScore = 0f;
        _ship = ship;
    }

    public void SetShip(Ship ship)
    {
        _ship = ship;
    }

    public void IncreaseScore(float amount)
    {
        CurrentScore += amount;
    }

    public void DecreaseScore(float amount)
    {
        var aux = CurrentScore - amount;
        CurrentScore = aux < 0 ? 0f : aux;
    }

    public Ship GetShip()
    {
        return _ship;
    }

    public float GetScore()
    {
        return CurrentScore;
    }
}