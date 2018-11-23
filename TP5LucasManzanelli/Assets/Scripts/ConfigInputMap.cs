using UnityEngine;
using UnityEngine.UI;

public class ConfigInputMap : ScriptableObject
{
    [System.Serializable]
    public class ConfigInput
    {
        public InputField InputField;
        public KeysConfiguration.Action Action;
        
    }
}