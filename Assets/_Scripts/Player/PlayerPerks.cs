using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerPerks : MonoBehaviour
{
    public Perk[] Perks;
    public int PerkCount = 3;

    [SerializeField] private PlayerController player;
    [SerializeField] private Perk[] _allPerks;


    private void Awake()
    {
        //GetAllPerks();
        //player.ShouldGetPerks = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || player.ShouldGetPerks)
        {
            GetRandomPerks();
        }
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

        player.ShouldGetPerks = false;
        GetPerkEffect();

    }

    public void GetPerkEffect()
    {
        foreach(Perk perk in Perks)
        {
            perk.Initialize(player);
        }
    }
}
