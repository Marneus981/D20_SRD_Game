using Cysharp.Threading.Tasks;

public interface IHeroActionFlow : IDependency<IHeroActionFlow>
{
    UniTask<CombatResult?> Play();
}

public class HeroActionFlow : IHeroActionFlow
{
    public async UniTask<CombatResult?> Play()
    {
        var menu = IActionMenu.Resolve();
        await menu.Setup();
        await menu.TransitionIn();
        var actionName = await menu.SelectMenuItem();
        UnityEngine.Debug.Log("Selected: " + actionName);
        await menu.TransitionOut();

        return ICombatResultSystem.Resolve().CheckResult();
    }
}