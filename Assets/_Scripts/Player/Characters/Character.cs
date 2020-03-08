using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Character", menuName = "Characters/New Character", order = 1)]
public class Character : ScriptableObject
{
    [Header("Base Stats")]
    public string Name = "New Character";
    public string Description = "Character's description";
    public float BaseHealth = 4;
    public float BaseSpeed = 5;
    public float BaseAccuracy = 1;
    public float BaseBulletSpeed = 1;
    public int BasePerkCount = 4;
    public int DashIndex = 1;

    public Image Icon;
    //public float DropProbability;

        // переделать
    public int SkinNumber;

    public Perk[] Perks;

    private Perk[] _allPerks;

    /*private void OnEnable()
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
        Perks = new Perk[BasePerkCount];
        for (int i = 0; i < BasePerkCount; i++)
        {
            // Take only from the latter part of the list - ignore the first i items.
            int take = Random.Range(i, _allPerks.Length);
            Perks[i] = _allPerks[take];

            // Swap our random choice to the beginning of the array,
            // so we don't choose it again on subsequent iterations.
            _allPerks[take] = _allPerks[i];
            _allPerks[i] = Perks[i];
        }
    }*/
}
