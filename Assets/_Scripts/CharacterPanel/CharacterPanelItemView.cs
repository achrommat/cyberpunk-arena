using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPanelItemView : MonoBehaviour
{
    public Button Button;
    public Image ItemIcon;
    public Text Description;

    private Character _characterData;

    public void InitItem(Character character)
    {
        _characterData = character;
        Description.text = character.Description;

        Button.onClick.AddListener(ButtonClicked);
    }

    private void ButtonClicked()
    {
        Debug.Log(_characterData.name);
    }
}