using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Character", menuName = "Characters/New Character", order = 1)]
public class Character : ScriptableObject
{
    public string Name = "New Character";
    public string Description = "Character's description";
    public Image Icon;
    public float DropProbability;
    public MeshRenderer Skin;

    public Perk[] Perks;
    public int PerkCount = 3;

    [SerializeField] private Perk[] _allPerks;

    private void OnEnable()
    {
        GetAllPerks();
        GetRandomPerks();
    }

    public void GetAllPerks()
    {
        _allPerks = Resources.FindObjectsOfTypeAll<Perk>();
    }

    private void GetRandomPerks()
    {
        Perks = new Perk[PerkCount];
        for (int i = 0; i < PerkCount; i++)
        {
            // Take only from the latter part of the list - ignore the first i items.
            int take = Random.Range(i, _allPerks.Length);
            Perks[i] = _allPerks[take];

            // Swap our random choice to the beginning of the array,
            // so we don't choose it again on subsequent iterations.
            _allPerks[take] = _allPerks[i];
            _allPerks[i] = Perks[i];
        }
    }
}
