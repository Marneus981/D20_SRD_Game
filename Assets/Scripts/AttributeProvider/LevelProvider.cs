using UnityEngine;

public class LevelProvider : MonoBehaviour, IAttributeProvider
{
    [SerializeField] int value;

    public void Setup(Entity entity)
    {
        entity.Level = value;
    }
}