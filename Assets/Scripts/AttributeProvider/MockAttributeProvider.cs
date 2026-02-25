using UnityEngine;

public class MockAttributeProvider : MonoBehaviour, IAttributeProvider
//Made to confirm expectations on calls
{
    public bool DidSetup { get; private set; }
    public Entity SetupEntity { get; private set; }

    public void Setup(Entity entity)
    {
        DidSetup = true;
        SetupEntity = entity;
    }
}