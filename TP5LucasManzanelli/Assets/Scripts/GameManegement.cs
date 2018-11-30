using System.Collections.Generic;
using controller;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManegement : MonoBehaviour
{
    public enum GameState
    {
        Init,
        Playing,
        Pause,
        Finish,
    }

    public float GlobalTimer;
    public World World;
    public MainMenuManagement MainMenuManagement;
    public GameObject SmallMeteorite;
    public GameObject WeapoGameObject;
    public GameObject HealthGameObject;
    public GameObject Ship;
    public List<Player.PlayerID> PlayersID;
    public GameState CurrentGameState;


    private void Start()
    {
        CurrentGameState = GameState.Init;
        GlobalTimer = GlobalTimer <= 0 ? 180f : GlobalTimer;
        if (Ship == null || Ship.GetComponent<Ship>() == null)
            return;
        PlayersID.ForEach(id =>
        {
            var player = new Player(id, "", GenerateShip());
            Store.Players.Add(id, player);
        });
        SetDefaultKeyCodesTwoPlayers();
    }

    private void Update()
    {
        if (CurrentGameState != GameState.Playing) return;
        if (CheckGameOver())
        {
            ChangeGameState(GameState.Finish);
            GameController.GameOver();
            MainMenuManagement.ShowResult(PlayersController.GetPlayersByScore());
            Debug.LogWarning("GAMEOVER! ");
            return;
        }

        GlobalTimer -= Time.deltaTime;
        GameController.CheckDestroyItems();
        GenerateRandomMeteorite(SmallMeteorite, 0.5f * Time.deltaTime);
        GenerateRandomPowerUps(WeapoGameObject, HealthGameObject, 0.5f * Time.deltaTime);
        ActionKeyConfig();
        PauseGame();
    }

    public void StartGame()
    {
        GameController.GameOver();
        foreach (var ship in Store.GetAllShips())
        {
            ((Ship) ship).RestoreLife();
            ((Ship) ship).Weapon.Upgrade = null;
            var pos = RandomPosition();
            ((Ship) ship).gameObject.transform.position = new Vector3(pos.X, pos.Y, 0f);
        }

        ChangeGameState(GameState.Playing);
    }

    private void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.Pause) && MainMenuManagement != null)
        {
            ChangeGameState(GameState.Playing);
            MainMenuManagement.PauseGame();
        }
    }

    private void ChangeGameState(GameState gameState)
    {
        CurrentGameState = gameState;
    }

    private bool CheckGameOver()
    {
        if (GlobalTimer <= 0f)
            return true;

//        var players = Store.Players.Values.Take(Store.Players.Values.Count - 1);
//        return players.Select(player => player.GetShip()).All(ship => ship == null || ship.Life <= 0);
        var ships = Store.GetAllShips();
        foreach (var s in ships)
        {
            if (s.CurrentStatus == Collisionable.Status.Destroy || s.GetCurrentLife() <= 0)
                return true;
        }

        return false;
    }

    private void ActionKeyConfig()
    {
        foreach (var keyConfig in Store.KeysConfigurationsMoves)
        {
            foreach (var keyCode in keyConfig.KeyCodes.Keys)
            {
                if (!Input.GetKey(keyCode)) continue;
                keyConfig.ActionKey(keyCode);
            }
        }

        foreach (var keyConfig in Store.KeysConfigurationsFired)
        {
            foreach (var keyCode in keyConfig.KeyCodes.Keys)
            {
                if (!Input.GetKeyDown(keyCode)) continue;
                keyConfig.ActionKey(keyCode);
            }
        }
    }

    private void GenerateRandomMeteorite(GameObject meteorite, float probability)
    {
        if (meteorite == null
            || meteorite.GetComponent<Meteorite>() == null
            || Store.GetMeteorites().Count > 9
            || Random.Range(0f, 1f) > probability)
            return;


        var generateMeteorite =
            Instantiate(meteorite, new Vector3(Random.Range(-100, 100), 100, 0), Quaternion.identity);
        generateMeteorite.gameObject.transform.SetParent(gameObject.transform);
        var scriptMeteorite = generateMeteorite.GetComponent<Meteorite>();
        RandomPositionLimits(scriptMeteorite);
//        Debug.Log("Direction: " + scriptMeteorite.GetDirection().ToStr());
        Store.SaveMeteorite(scriptMeteorite);
    }

    private void GenerateRandomPowerUps(GameObject weapon, GameObject health, float probability)
    {
        if (weapon != null)
            Generate(weapon, probability);
        if (health != null)
            Generate(health, probability);
    }

    private void Generate(GameObject gObject, float probability)
    {
        if (gObject == null
            || Store.GetPowerUps().Count > 3
            || Random.Range(0f, 1f) > probability)
            return;
        var pos = RandomPosition();
        var generate = Instantiate(gObject, new Vector3(pos.X, pos.Y, 0f), Quaternion.identity);
        generate.gameObject.transform.SetParent(gameObject.transform);
        var scriptColisionable = generate.GetComponent<Collisionable>();
        scriptColisionable.Position = pos;
        scriptColisionable.SetDirection(Movement.RandomDirection());
        Store.SavePowerUp(scriptColisionable);
        Debug.Log("Power UP!");
    }

    private Ship GenerateShip()
    {
        var pos = RandomPosition();
        var ship = Instantiate(Ship, new Vector3(pos.X, pos.Y, 0f), Quaternion.identity);
//        ship.gameObject.transform.SetParent(gameObject.transform);
        var shipScript = ship.GetComponent<Ship>();
        shipScript.Position = pos;
        return shipScript;
    }

    private Vector2 RandomPosition()
    {
        return new Vector2(Random.Range(-50, 50), Random.Range(-25, 25));
//        var z = World.Camera.transform.position.z; 
//        return new Vector2(Random.Range(-z, z), Random.Range(-(z/2), z/2));
    }

    private void RandomPositionLimits(Collisionable collisionable)
    {
        var dir = Random.Range(0, 2) * 2 - 1;
        if (Random.Range(0f, 1f) <= 0.5f)
        {
            collisionable.Position = (new Vector2(Random.Range(-50, 50), 25 * dir));
            collisionable.SetDirection(new Vector2(0, dir * -1));
        }
        else
        {
            collisionable.Position = (new Vector2(50 * dir, Random.Range(-25, 25)));
            collisionable.SetDirection(new Vector2(dir * -1, 0));
        }
    }

    private void SetDefaultKeyCodesTwoPlayers()
    {
        var keyConfig1 = new KeysConfiguration(Player.PlayerID.Player1)
        {
            KeyCodes = new Dictionary<KeyCode, KeysConfiguration.Action>
            {
                {KeyCode.A, KeysConfiguration.Action.MoveLeft},
                {KeyCode.D, KeysConfiguration.Action.MoveRight},
                {KeyCode.S, KeysConfiguration.Action.MoveDown},
                {KeyCode.W, KeysConfiguration.Action.MoveUp},
            }
        };
        var keyConfig1Fired = new KeysConfiguration(Player.PlayerID.Player1)
        {
            KeyCodes = new Dictionary<KeyCode, KeysConfiguration.Action>
            {
                {KeyCode.Space, KeysConfiguration.Action.Fired},
            }
        };
        Store.KeysConfigurationsMoves.Add(keyConfig1);
        Store.KeysConfigurationsFired.Add(keyConfig1Fired);

        var keyConfig2 = new KeysConfiguration(Player.PlayerID.Player2)
        {
            KeyCodes = new Dictionary<KeyCode, KeysConfiguration.Action>
            {
                {KeyCode.LeftArrow, KeysConfiguration.Action.MoveLeft},
                {KeyCode.RightArrow, KeysConfiguration.Action.MoveRight},
                {KeyCode.DownArrow, KeysConfiguration.Action.MoveDown},
                {KeyCode.UpArrow, KeysConfiguration.Action.MoveUp},
            }
        };

        var keyConfig2Fired = new KeysConfiguration(Player.PlayerID.Player2)
        {
            KeyCodes = new Dictionary<KeyCode, KeysConfiguration.Action>
            {
                {KeyCode.Keypad0, KeysConfiguration.Action.Fired}
            }
        };
        Store.KeysConfigurationsMoves.Add(keyConfig2);
        Store.KeysConfigurationsFired.Add(keyConfig2Fired);
    }
}