using UnityEngine;

public class HealthProvider : MonoBehaviour, IAttributeProvider
{
    [SerializeField] int value;

    public void Setup(Entity entity)
    {
        entity.HitPoints = value;
        entity.MaxHitPoints = value;
    }
}