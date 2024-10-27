using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChatAreaScript : MonoBehaviour
{
    [SerializeField]
    TMP_Text CharacterText;

    public void SettingText(string text)
    {
        CharacterText.text = text;
    }

}
