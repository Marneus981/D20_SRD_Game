using UnityEngine;
using Cysharp.Threading.Tasks;

public class Stride : MonoBehaviour, ICombatAction
{
    public async UniTask Perform(Entity entity)
    {
        // TODO: differentiate between user-controlled and AI controlled entities
        var position = await IPositionSelectionSystem.Resolve().Select(entity.Position);

        var info = new StrideInfo
        {
            entity = entity,
            destination = position
        };

        await IStrideSystem.Resolve().Apply(info);
    }
}