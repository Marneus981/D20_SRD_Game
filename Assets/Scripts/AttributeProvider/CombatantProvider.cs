using UnityEngine;

public class CombatantProvider : MonoBehaviour, IAttributeProvider
{
    public void Setup(Entity entity)
    {
        entity.IsCombatant = true;
    }
}