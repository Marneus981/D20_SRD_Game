using UnityEngine;

public class DamageImmunityProvider : MonoBehaviour, IAttributeProvider
{
    [SerializeField] string damageType;

    public void Setup(Entity entity)
    {
        IDamageImmunitySystem.Resolve().AddImmunity(entity, damageType);
    }
}