using UnityEngine;

public class AbilityScoreProvider : MonoBehaviour, IAttributeProvider
/*
Provide a fixed ability score. 
Use Example: add attributes for specified scores a creature lacks.
*/
{
    [SerializeField] AbilityScore.Attribute attribute;
    [SerializeField] int value;

    public void Setup(Entity entity)
    {
        entity[attribute] = value;
    }
/*     public void Configure(AbilityScore.Attribute attribute, int value)
    {
        this.attribute = attribute;
        this.value = value;
    } */
}