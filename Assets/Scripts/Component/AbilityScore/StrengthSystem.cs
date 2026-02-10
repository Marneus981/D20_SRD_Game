public partial class Data
{
    //Mapping from an Entity to an AbilityScore (the CoreDictionary named strength).
    public CoreDictionary<Entity, AbilityScore> strength = new CoreDictionary<Entity, AbilityScore>();
}

public interface IStrengthSystem : IDependency<IStrengthSystem>, IEntityTableSystem<AbilityScore>
/*
Inherits from IDependency so that it is injectable, 
and from IEntityTableSystem to inherit some basic “Table” related functionality.
*/
{

}

public class StrengthSystem : EntityTableSystem<AbilityScore>, IStrengthSystem
/*
Inherits from EntityTableSystem and conforms to our IStrengthSystem interface.
*/
{
    public override CoreDictionary<Entity, AbilityScore> Table => IDataSystem.Resolve().Data.strength;
}

public partial struct Entity
/*
Convenience partial definition of Entity so that we get the Strength as a wrapped property of our system.
*/
{
    public AbilityScore Strength
    {
        get { return IStrengthSystem.Resolve().Get(this); }
        set { IStrengthSystem.Resolve().Set(this, value); }
    }
}