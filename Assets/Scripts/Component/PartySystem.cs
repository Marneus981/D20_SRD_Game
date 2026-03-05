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
    Party OpposingParty(Party party);
}

public class PartySystem : EntityTableSystem<Party>, IPartySystem
{
    public override CoreDictionary<Entity, Party> Table => IDataSystem.Resolve().Data.party;
    public Party OpposingParty(Party party)
    {
        return party == Party.Hero ? Party.Monster : Party.Hero;
    }
}

public partial struct Entity
{
    public Party Party
    {
        get { return IPartySystem.Resolve().Get(this); }
        set { IPartySystem.Resolve().Set(this, value); }
    }
}
public static class PartyExtensions
{
    public static Party OpposingParty(this Party party)
    {
        return IPartySystem.Resolve().OpposingParty(party);
    }
}