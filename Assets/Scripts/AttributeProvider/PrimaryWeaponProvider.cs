using UnityEngine;
using Cysharp.Threading.Tasks;

public class PrimaryWeaponProvider : MonoBehaviour, IAttributeProvider
{
    public string recipeName;

    public async UniTask SetupFlow(Entity entity)
    {
        var weapon = await IWeaponAssetSystem.Resolve().Spawn(recipeName);
        entity.PrimaryHand = weapon;
    }
}