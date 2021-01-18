using UnityEngine;

[CreateAssetMenu(fileName = "Progression", menuName = "Stats/New Progression", order = 0)]
public class Progression : ScriptableObject
{
    [SerializeField] ProgressionCharacterClass characterClass;
    [System.Serializable] class ProgressionCharacterClass
    {
        [SerializeField] int value = 3;
        [SerializeField] string name = "Hello";
    }
}