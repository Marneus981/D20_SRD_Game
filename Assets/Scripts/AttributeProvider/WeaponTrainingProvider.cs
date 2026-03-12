using UnityEngine;

public class WeaponTrainingProvider : MonoBehaviour, IAttributeProvider
{
    public WeaponTraining value;

    public void Setup(Entity entity)
    {
        IWeaponProficiencySystem.Resolve().AddWeaponTraining(value, entity);
    }
}