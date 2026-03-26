using Cysharp.Threading.Tasks;

public interface IHeroActionFlow : IDependency<IHeroActionFlow>
{
    UniTask<CombatResult?> Play();
}
[Dependency(typeof(IHeroActionFlow))]
public class HeroActionFlow : IHeroActionFlow
{
    public async UniTask<CombatResult?> Play()
    {
        var hero = ITurnSystem.Resolve().Current;
        /* ICombatSelectionIndicator.Resolve().SetPosition(hero.Position); */
        ICombatSelectionIndicator.Resolve().Mark(hero);
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