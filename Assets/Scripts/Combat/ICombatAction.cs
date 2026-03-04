using Cysharp.Threading.Tasks;

public interface ICombatAction
{
    UniTask Perform(Entity entity);
    bool CanPerform(Entity entity);
}