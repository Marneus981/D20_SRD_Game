using System.Collections.Generic;
using UnityEngine;

public class EncounterActionsProvider : MonoBehaviour, IAttributeProvider
{
    [SerializeField] List<string> value;

    public void Setup(Entity entity)
    {
/*         entity.EncounterActions = value; */
        entity.EncounterActions = new EncounterActions(value);
    }
}