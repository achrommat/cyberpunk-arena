using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Perk : ScriptableObject
{
    public string Name = "New Perk";
    public string Description = "Perk's description";
    public Image Icon;
    public float DropProbability;

    public virtual void Initialize(PlayerController player)
    {
    }
}