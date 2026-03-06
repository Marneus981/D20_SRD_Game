using Cysharp.Threading.Tasks;

public interface ICreateHeroPartyFlow : IDependency<ICreateHeroPartyFlow>
{
    UniTask Play();
}

public class CreateHeroPartyFlow : ICreateHeroPartyFlow
{
    const int heroPartySize = 4;

    public async UniTask Play()
    {
        for (int i = 0; i < heroPartySize; ++i)
        {
            var entity = await IEntityRecipeSystem.Resolve().Create("Hero");
            entity.PartyOrder = i;
        }
        await UniTask.CompletedTask;
    }
}