public partial class Data
{
    public CoreSet<Entity> combatant = new CoreSet<Entity>();
}

public interface ICombatantSystem : IDependency<ICombatantSystem>, IEntitySetSystem
{

}

public class CombatantSystem : EntitySetSystem, ICombatantSystem
{
    public override CoreSet<Entity> Table => IDataSystem.Resolve().Data.combatant;
}

public partial struct Entity
{
    public bool IsCombatant
    {
        get { return ICombatantSystem.Resolve().Contains(this); }
        set
        {
            if (value)
                ICombatantSystem.Resolve().Add(this);
            else
                ICombatantSystem.Resolve().Remove(this);
        }
    }
}