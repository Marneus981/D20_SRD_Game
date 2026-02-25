using Cysharp.Threading.Tasks;

public partial class Data
{
    public Entity hero;
}

public interface ISoloHeroSystem : IDependency<ISoloHeroSystem>
{
    Entity Hero { get; }
    UniTask CreateHero();
}

public class SoloHeroSystem : ISoloHeroSystem
{
    public Entity Hero
    {
        get { return IDataSystem.Resolve().Data.hero; }
        private set { IDataSystem.Resolve().Data.hero = value; }
    }

    public async UniTask CreateHero()
    {
        Hero = await IEntityRecipeSystem.Resolve().Create("Hero");
        ISkillSystem.Resolve().SetupAllSkills(Hero);
    }
}