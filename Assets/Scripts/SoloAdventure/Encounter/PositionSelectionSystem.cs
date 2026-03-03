using Cysharp.Threading.Tasks;

public interface IPositionSelectionSystem : IDependency<IPositionSelectionSystem>
{
    UniTask<Point> Select(Point start);
}

public class PositionSelectionSystem : IPositionSelectionSystem
{
    public async UniTask<Point> Select(Point start)
    {
        var result = start;
        var indicator = ICombatSelectionIndicator.Resolve();
        indicator.SetPosition(start);

        var input = IInputSystem.Resolve();
        while (true)
        {
            await UniTask.NextFrame();
            if (input.GetKeyUp(InputAction.Confirm))
                break;
            result += new Point(
                input.GetAxisUp(InputAxis.Horizontal),
                input.GetAxisUp(InputAxis.Vertical)
                );
            indicator.SetPosition(result);
        }

        return result;
    }
}