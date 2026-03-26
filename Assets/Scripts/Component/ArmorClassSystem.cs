public partial class Data
{
    public CoreDictionary<Entity, int> armorClass = new CoreDictionary<Entity, int>();
}

public interface IArmorClassSystem : IDependency<IArmorClassSystem>, IEntityTableSystem<int>
{

}

[Dependency(typeof(IArmorClassSystem))]
public class ArmorClassSystem : EntityTableSystem<int>, IArmorClassSystem
{
    public override CoreDictionary<Entity, int> Table => IDataSystem.Resolve().Data.armorClass;
}

public partial struct Entity
{
    public int ArmorClass
    {
        get { return IArmorClassSystem.Resolve().Get(this); }
        set { IArmorClassSystem.Resolve().Set(this, value); }
    }
}