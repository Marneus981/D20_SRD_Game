using UnityEngine;

public class AbilityScoresProvider : MonoBehaviour, IAttributeProvider
{
    [SerializeField] int strength;
    [SerializeField] int dexterity;
    [SerializeField] int constitution;
    [SerializeField] int intelligence;
    [SerializeField] int wisdom;
    [SerializeField] int charisma;

    public void Setup(Entity entity)
    {
        entity.Strength = strength;
        entity.Dexterity = dexterity;
        entity.Constitution = constitution;
        entity.Intelligence = intelligence;
        entity.Wisdom = wisdom;
        entity.Charisma = charisma;
    }
}