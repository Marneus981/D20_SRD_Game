using Cysharp.Threading.Tasks;

public interface ICombatAction
{
    UniTask Perform(Entity entity);
}