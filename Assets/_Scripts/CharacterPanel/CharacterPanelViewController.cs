using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CharacterPanelViewController : MonoBehaviour
{
    [SerializeField] private PlayerCharacter _playerCharacter;
    [SerializeField] private Transform _characterPanel;
    [SerializeField] private CharacterPanelItemView[] _characterItems;
    [SerializeField] private int _characterCount;
    [SerializeField] private List<Character> _allCharacters;
    public Character[] Characters;

    void OnEnable()
    {
        GetAllCharacters();
        GetRandomCharacters();
    }

    public void GetAllCharacters()
    {
        string[] assetNames = AssetDatabase.FindAssets("t:Character", new[] { "Assets/_ScriptableObjects/Characters" });
        _allCharacters.Clear();
        foreach (string SOName in assetNames)
        {
            var SOpath = AssetDatabase.GUIDToAssetPath(SOName);
            var character = AssetDatabase.LoadAssetAtPath<Character>(SOpath);
            _allCharacters.Add(character);
        }
    }

    private void GetRandomCharacters()
    {
        Characters = new Character[_characterCount];
        for (int i = 0; i < _characterCount; i++)
        {
            // Take only from the latter part of the list - ignore the first i items.
            int take = Random.Range(i, _allCharacters.Count);
            Characters[i] = _allCharacters[take];

            // Swap our random choice to the beginning of the array,
            // so we don't choose it again on subsequent iterations.
            _allCharacters[take] = _allCharacters[i];
            _allCharacters[i] = Characters[i];
        }

        InitializeCharacterPanelItem();

    }

    private void InitializeCharacterPanelItem()
    {
        _characterItems = _characterPanel.GetComponentsInChildren<CharacterPanelItemView>();
        for (int i = 0; i < _characterItems.Length; i++)
        {
            _characterItems[i].InitItem(Characters[i]);
        }
    }
}
