using UnityEngine;

public class DamageWeaknessProvider : MonoBehaviour, IAttributeProvider
{
    [SerializeField] string damageType;
    [SerializeField] int amount;

    public void Setup(Entity entity)
    {
        IDamageWeaknessSystem.Resolve().SetWeakness(entity, damageType, amount);
    }
}