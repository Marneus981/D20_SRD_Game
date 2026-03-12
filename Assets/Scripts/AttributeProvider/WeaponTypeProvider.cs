using UnityEngine;

public class WeaponTypeProvider : MonoBehaviour, IAttributeProvider
{
    public WeaponType value;

    public void Setup(Entity entity)
    {
        entity.WeaponType = value;
    }
}