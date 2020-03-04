using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Current Player Character Data", menuName = "Characters/Current Player Character Data", order = 2)]
public class CurrentPlayerCharacterData : ScriptableObject
{
    public Character Character;
}
