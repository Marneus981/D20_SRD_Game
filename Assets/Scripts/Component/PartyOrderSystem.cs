public partial class Data
{
    public CoreDictionary<Entity, int> partyOrder = new CoreDictionary<Entity, int>();
}

public interface IPartyOrderSystem : IDependency<IPartyOrderSystem>, IEntityTableSystem<int>
{
    Entity PartyLeader { get; }
}

public class PartyOrderSystem : EntityTableSystem<int>, IPartyOrderSystem
{
    public override CoreDictionary<Entity, int> Table => IDataSystem.Resolve().Data.partyOrder;

    public Entity PartyLeader
    {
        get
        {
            foreach (var entity in Table.Keys)
            {
                if (entity.PartyOrder == 0)
                    return entity;
            }
            return Entity.None;
        }
    }
}

public partial struct Entity
{
    public int PartyOrder
    {
        get { return IPartyOrderSystem.Resolve().Get(this); }
        set { IPartyOrderSystem.Resolve().Set(this, value); }
    }
}