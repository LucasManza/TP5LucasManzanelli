using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using view;

public class MainMenuManagement : MonoBehaviour
{
    public List<MenuView> Menus = new List<MenuView>();
    private bool _pauseGame;

    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Pause)) return;
        if (_pauseGame)
        {
            PauseGame();
            _pauseGame = true;
        }
        else
        {
            StartGame();
            _pauseGame = false;
        }
    }

    private void Start()
    {
        ShowMainMenu();
    }

    public void StartGame()
    {
        EnableView(MenuView.MenuId.None, "");
    }

    public void PauseGame()
    {
        EnableView(MenuView.MenuId.PauseMenu, "Pause");
    }

    public void ShowMainMenu()
    {
        EnableView(MenuView.MenuId.MainMenu, "");
    }

    public void ShowConfiguration()
    {
        EnableView(MenuView.MenuId.ConfigurationMenu, "");

    }

    public void ShowResult(List<Player> players)
    {
        var result = "";
        players.ForEach(p => { });
        for (var i = 0; i < players.Count; i++)
        {
            result += (i + 1) + "° PlayerID: " + players[i].ID + " Name: " + players[i].NickName + " Score: " +
                      players[i].CurrentScore + '\n';
        }

        EnableView(MenuView.MenuId.ResultMenu, result);
    }

    private void EnableView(MenuView.MenuId menuId, string txt)
    {
        Menus.ForEach(m =>
        {
            var show = (m.Id == menuId);
            m.gameObject.SetActive(show);
            if (show)
                m.ShowText(txt);
        });
    }

    private void PrintError(string str)
    {
        Debug.LogError("ERROR TO LOAD MENU: " + str + " !");
    }
}