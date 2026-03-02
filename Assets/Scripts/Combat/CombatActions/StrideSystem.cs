using Cysharp.Threading.Tasks;

public struct StrideInfo
{
    public Entity entity;
    public Point destination;
}

public interface IStrideSystem : IDependency<IStrideSystem>
{
    UniTask Apply(StrideInfo info);
}

public class StrideSystem : IStrideSystem
{
    public async UniTask Apply(StrideInfo info)
    {
        // TODO: Check for act of opportunity before leaving current square
        await Present(info);
        Perform(info);
        // TODO: Check for act of opportunity after arriving at new square
    }

    private async UniTask Present(StrideInfo info)
    {
        IStridePresenter presenter;
        if (IStridePresenter.TryResolve(out presenter))
        {
            var presentInfo = new StridePresentationInfo
            {
                entity = info.entity,
                fromPosition = info.entity.Position,
                toPosition = info.destination
            };
            await presenter.Present(presentInfo);
        }
    }

    private void Perform(StrideInfo info)
    {
        var entity = info.entity;
        entity.Position = info.destination;
        ITurnSystem.Resolve().TakeAction(1, false);
    }
}