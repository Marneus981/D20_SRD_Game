using UnityEngine;

public class PerceptionProficiencyProvider : MonoBehaviour, IAttributeProvider
{
    public Proficiency proficiency;

    public void Setup(Entity entity)
    {
        IPerformanceProficiencySystem.Resolve().Set(entity, proficiency);
    }
}