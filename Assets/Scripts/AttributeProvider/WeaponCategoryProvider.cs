using UnityEngine;

public class WeaponCategoryProvider : MonoBehaviour, IAttributeProvider
{
    public WeaponCategory value;

    public void Setup(Entity entity)
    {
        entity.WeaponCategory = value;
    }
}