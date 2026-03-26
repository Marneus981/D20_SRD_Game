using System.Collections.Generic;
/*
Placeholder: System that can provide the list of actions an Entity can perform during an encounter. 
This may be a subset of the actions that the Entity can actually perform; some actions may only be
allowed during a different phase of the game such as exploration.
*/
[System.Serializable]
public struct EncounterActions
{
    public List<string> names;

    public EncounterActions(List<string> names)
    {
        this.names = names;
    }
}

public partial class Data
{
    public CoreDictionary<Entity, EncounterActions> encounterActions = new CoreDictionary<Entity, EncounterActions>();
}

public interface IEncounterActionsSystem : IDependency<IEncounterActionsSystem>, IEntityTableSystem<EncounterActions>
{

}
[Dependency(typeof(IEncounterActionsSystem))]
public class EncounterActionsSystem : EntityTableSystem<EncounterActions>, IEncounterActionsSystem
{
    public override CoreDictionary<Entity, EncounterActions> Table => IDataSystem.Resolve().Data.encounterActions;
}

public partial struct Entity
{
    public EncounterActions EncounterActions
    {
        get { return IEncounterActionsSystem.Resolve().Get(this); }
        set { IEncounterActionsSystem.Resolve().Set(this, value); }
    }
}