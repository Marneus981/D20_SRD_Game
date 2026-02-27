using Cysharp.Threading.Tasks;

public interface IHeroActionFlow : IDependency<IHeroActionFlow>
{
    UniTask<CombatResult?> Play();
}

public class HeroActionFlow : IHeroActionFlow
{
    public async UniTask<CombatResult?> Play()
    {
        var hero = ISoloHeroSystem.Resolve().Hero;
        ICombatSelectionIndicator.Resolve().SetPosition(hero.Position);
        ICombatSelectionIndicator.Resolve().SetVisible(true);

        var menu = IActionMenu.Resolve();
        await menu.Setup();
        await menu.TransitionIn();
        var actionName = await menu.SelectMenuItem();
        await menu.TransitionOut();

        var action = await ICombatActionAssetSystem.Resolve().Load(actionName);
        await action.Perform(hero);

        ICombatSelectionIndicator.Resolve().SetVisible(false);
        return ICombatResultSystem.Resolve().CheckResult();
    }
}