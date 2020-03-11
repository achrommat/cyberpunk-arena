using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public static Character Character;

    [SerializeField] private Transform _skinFolder;
    [SerializeField] private PlayerController _player;

    [SerializeField] private List<Renderer> _skinList;
    [SerializeField] private Outline _outline;
    private float _outlineAlpha;

    private void Awake()
    {
        _outlineAlpha = _outline.outlineColor.a;
    }

    public void GenerateCharacter()
    {
        GenerateAppearance();
        GenerateBaseStats();
        GeneratePerks();
    }

    private void GenerateAppearance()
    {
        Renderer[] skins = _skinFolder.GetComponentsInChildren<Renderer>(true);
       
        for (int i = 0; i < skins.Length; i++)
        {
            if ((skins[i].name.Contains("Character_") || skins[i].name.Contains("SM_Chr")))
            {
                skins[i].gameObject.SetActive(false);
                _skinList.Add(skins[i]);
            }
        }

        for (int i = 0; i < _skinList.Count; i++)
        {
            if (Character.SkinNumber == i)
            {
                _skinList[i].gameObject.SetActive(true);
            }
        }

    }

    private void GenerateBaseStats()
    {
        _player.stats.Health = Character.BaseHealth;
        _player.stats.CurrentHealth = Character.BaseHealth;
        HealthBarViewController.PlayerStats = _player.stats;

        _player.stats.Accuracy = Character.BaseAccuracy;
        _player.stats.BulletSpeed = Character.BaseBulletSpeed;
        _player.stats.RunSpeed = Character.BaseSpeed;
        _player.stats.CurrentRunSpeed = Character.BaseSpeed;
        _player.DashIndex = Character.DashIndex;
    }

    private void GeneratePerks()
    {

    }

}
