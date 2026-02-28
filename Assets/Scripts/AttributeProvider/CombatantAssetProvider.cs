using UnityEngine;

public class CombatantAssetProvider : MonoBehaviour, IAttributeProvider
{
    [SerializeField] string value;

    public void Setup(Entity entity)
    {
        entity.CombatantAsset = value;
    }
}