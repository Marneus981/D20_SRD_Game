using UnityEngine;
using Cysharp.Threading.Tasks;

public class Stride : MonoBehaviour, ICombatAction
{
    public async UniTask Perform(Entity entity)
    {
        // TODO: differentiate between user-controlled and AI controlled entities

        // TODO: can pass dynamic obstacle positions, such as of enemy units
        var traverser = new LandTraverser(null);

        var map = IPathfindingSystem.Resolve().Map(
            entity.Position,
            entity.Speed,
            traverser);

        IBoardHighlightSystem.Resolve().Highlight(
            map.AllPoints(),
            new Color(0, 1, 1, 0.5f));

        var position = await IPositionSelectionSystem.Resolve().Select(entity.Position);
        IBoardHighlightSystem.Resolve().ClearHighlights();

        var info = new StrideInfo
        {
            entity = entity,
            path = map.GetPathToPoint(position)
        };

        await IStrideSystem.Resolve().Apply(info);
    }
}