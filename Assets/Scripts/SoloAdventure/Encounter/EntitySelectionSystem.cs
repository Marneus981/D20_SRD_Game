using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using System;

public interface IEntitySelectionSystem : IDependency<IEntitySelectionSystem>
{
    UniTask<Entity> Select(List<Entity> list);
}
[Dependency(typeof(IEntitySelectionSystem))]
public class EntitySelectionSystem : IEntitySelectionSystem
{
    public async UniTask<Entity> Select(List<Entity> list)
    {
        if (list == null || list.Count == 0)
            return new Entity(0);

        var selection = 0;
        Mark(list[selection]);

        var input = IInputSystem.Resolve();
        //listen for input...
        while (true)
        {
            await UniTask.NextFrame();
            if (input.GetKeyUp(InputAction.Confirm))
                break;

            var hor = input.GetAxisUp(InputAxis.Horizontal);
            var ver = input.GetAxisUp(InputAxis.Vertical);
            if (hor == 0 && ver == 0)
                continue;

            if (hor > 0 || ver > 0)
                selection++;
            else if (hor < 0 || ver < 0)
                selection--;

            if (selection < 0) selection += list.Count;
            if (selection >= list.Count) selection -= list.Count;

            Mark(list[selection]);
        }
        return list[selection];
    }

    void Mark(Entity entity)
    {
        var indicator = ICombatSelectionIndicator.Resolve();
        indicator.Mark(entity);
    }
}