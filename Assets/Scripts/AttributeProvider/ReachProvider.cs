using UnityEngine;

public class ReachProvider : MonoBehaviour, IAttributeProvider
{
    [SerializeField] Reach value;

    public void Setup(Entity entity)
    {
        entity.Reach = value;
    }
}