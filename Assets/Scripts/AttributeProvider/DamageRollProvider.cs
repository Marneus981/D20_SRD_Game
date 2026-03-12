using UnityEngine;

public class DamageRollProvider : MonoBehaviour, IAttributeProvider
{
    public DiceRoll value;

    public void Setup(Entity entity)
    {
        entity.DamageRoll = value;
    }
}