using UnityEngine;
using UnityEngine.UI;

namespace view
{
    public class MenuView : MonoBehaviour
    {
        public enum MenuId
        {
            None,
            MainMenu,
            ResultMenu,
            ConfigurationMenu,
            PauseMenu
        }

        public MenuId Id = MenuId.None;
        public Text Text;

        public bool Show(bool show)
        {
            gameObject.SetActive(show);
            return show;
        }

        public void ShowText(string txt)
        {
            if (Text != null)
                Text.text = txt;
        }
    }
}