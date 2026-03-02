public enum Party
{
    None,
    Hero,
    Monster
}

public partial class Data
{
    public CoreDictionary<Entity, Party> party = new CoreDictionary<Entity, Party>();
}

public interface IPartySystem : IDependency<IPartySystem>, IEntityTableSystem<Party>
{

}

public class PartySystem : EntityTableSystem<Party>, IPartySystem
{
    public override CoreDictionary<Entity, Party> Table => IDataSystem.Resolve().Data.party;
}

public partial struct Entity
{
    public Party Party
    {
        get { return IPartySystem.Resolve().Get(this); }
        set { IPartySystem.Resolve().Set(this, value); }
    }
}