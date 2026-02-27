using System.Collections.Generic;
/*
Placeholder: System that can provide the list of actions an Entity can perform during an encounter. 
This may be a subset of the actions that the Entity can actually perform; some actions may only be
allowed during a different phase of the game such as exploration.
*/
public interface IEncounterActionsSystem : IDependency<IEncounterActionsSystem>, IEntityTableSystem<List<string>>
{

}

public class EncounterActionsSystem : EntityTableSystem<List<string>>, IEncounterActionsSystem
{
    public override CoreDictionary<Entity, List<string>> Table => _table;
    CoreDictionary<Entity, List<string>> _table = new CoreDictionary<Entity, List<string>>();
}

public partial struct Entity
{
    public List<string> EncounterActions
    {
        get { return IEncounterActionsSystem.Resolve().Get(this); }
        set { IEncounterActionsSystem.Resolve().Set(this, value); }
    }
}