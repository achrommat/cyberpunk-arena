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
    public bool IsInitialized = false;

    public Perk[] Perks;

    private List<Perk> _allPerks;
    public int BasePerkCount = 4;

    public void InitItem(Character character)
    {
        _characterData = character;
        Description.text = character.Description;

        if (!IsInitialized)
        {
            Button.onClick.AddListener(ButtonClicked);
            IsInitialized = true;
        }       
    }

    private void ButtonClicked()
    {
        PlayerCharacter.Character = _characterData;
        OnCharacterChosen.Invoke();
    }

    /*public void GetAllPerks()
    {
        string[] assetNames = AssetDatabase.FindAssets("t:Character", new[] { "Assets/_ScriptableObjects/Perks/Passive" });
        _allPerks.Clear();
        foreach (string SOName in assetNames)
        {
            var SOpath = AssetDatabase.GUIDToAssetPath(SOName);
            var perk = AssetDatabase.LoadAssetAtPath<Perk>(SOpath);
            _allPerks.Add(perk);
        }
    }

    private void GetRandomPerks()
    {
        Perks = new Perk[BasePerkCount];
        for (int i = 0; i < BasePerkCount; i++)
        {
            // Take only from the latter part of the list - ignore the first i items.
            int take = Random.Range(i, _allPerks.Count);
            Perks[i] = _allPerks[take];

            // Swap our random choice to the beginning of the array,
            // so we don't choose it again on subsequent iterations.
            _allPerks[take] = _allPerks[i];
            _allPerks[i] = Perks[i];
        }
    }*/

}