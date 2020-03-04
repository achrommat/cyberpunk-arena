using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CharacterPanelItemView : MonoBehaviour
{
    public Button Button;
    public Image ItemIcon;
    public Text Description;

    [SerializeField] private Character _characterData;
    public UnityEvent OnCharacterChosen;

    public void InitItem(Character character)
    {
        _characterData = character;
        Description.text = character.Description;

        Button.onClick.AddListener(ButtonClicked);
    }

    private void ButtonClicked()
    {
        PlayerCharacter.Character = _characterData;
        OnCharacterChosen.Invoke();
    }
}