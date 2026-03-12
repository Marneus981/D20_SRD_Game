using UnityEngine;

public class LevelProvider : MonoBehaviour, IAttributeProvider
{
    public int value;//[SerializeField] int value;

    public void Setup(Entity entity)
    {
        entity.Level = value;
    }
}