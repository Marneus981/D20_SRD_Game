using UnityEngine;

public class SizeProvider : MonoBehaviour, IAttributeProvider
{
    [SerializeField] Size value;

    public void Setup(Entity entity)
    {
        entity.Size = value;
    }
}