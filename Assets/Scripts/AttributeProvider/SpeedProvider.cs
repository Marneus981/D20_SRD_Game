using UnityEngine;

public class SpeedProvider : MonoBehaviour, IAttributeProvider
{
    [SerializeField] int value;

    public void Setup(Entity entity)
    {
        entity.Speed = value;
    }
}