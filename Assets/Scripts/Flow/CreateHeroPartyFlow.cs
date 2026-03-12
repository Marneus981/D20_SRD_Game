using Cysharp.Threading.Tasks;

public interface ICreateHeroPartyFlow : IDependency<ICreateHeroPartyFlow>
{
    UniTask Play();
}

public class CreateHeroPartyFlow : ICreateHeroPartyFlow
{
    const int heroPartySize = 4;

    string[] autoChosenAncestries = new string[] {
        "Dwarf",
        "Elf",
        "Gnome",
        "Halfling"
    };

    public async UniTask Play()
    {
        for (int i = 0; i < heroPartySize; ++i)
        {
            var entity = await IEntityRecipeSystem.Resolve().Create("Hero");
            entity.PartyOrder = i;
            await LoadAncestry(entity, autoChosenAncestries[i]);
            await LoadBackground(entity);
            ISkillSystem.Resolve().SetupAllSkills(entity);
            ISavingThrowSystem.Resolve().SetupAllSavingThrows(entity);
            IPerceptionSystem.Resolve().Setup(entity);
        }
        await UniTask.CompletedTask;
    }
    async UniTask LoadAncestry(Entity entity, string ancestry)
    {
        UnityEngine.Debug.Log(string.Format("Loading Ancestry: {0}", ancestry));
        entity.Ancestry = ancestry;
        var assetSystem = IAncestryAssetSystem.Resolve();
        var ancestryAsset = await assetSystem.Load(ancestry);
        foreach (var provider in ancestryAsset.AttributeProviders)
        {
            await provider.SetupFlow(entity);//provider.Setup(entity);
        }
    }
    async UniTask LoadBackground(Entity entity)
    {
        UnityEngine.Debug.Log(string.Format("Loading Background: {0}", entity.Background));
        var backgroundAsset = await IBackgroundAssetSystem.Resolve().Load(entity.Background);
        foreach (var provider in backgroundAsset.AttributeProviders)
        {
            await provider.SetupFlow(entity);//provider.Setup(entity);
        }
    }
}