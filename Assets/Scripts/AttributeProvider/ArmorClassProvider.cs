using UnityEngine;

public class ArmorClassProvider : MonoBehaviour, IAttributeProvider
{
    [SerializeField] int value;

    public void Setup(Entity entity)
    {
        entity.ArmorClass = value;
    }
}