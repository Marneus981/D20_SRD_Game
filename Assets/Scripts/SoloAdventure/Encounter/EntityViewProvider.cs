using System.Collections.Generic;
using UnityEngine;
//Class to map GamObject to Entity and its Zone(ViewZone)
public enum ViewZone
{
    Combatant//more to be added (e.g. chara portraits, etc.)
    //Maybe add separate healthbar view?
}
public interface IEntityViewProvider : IDependency<IEntityViewProvider>
{
    GameObject GetView(Entity entity, ViewZone zone);
    void SetView(GameObject view, Entity entity, ViewZone zone);
}
public class EntityViewProvider : MonoBehaviour, IEntityViewProvider
{
    Dictionary<ViewZone, Dictionary<Entity, GameObject>> mapping = new Dictionary<ViewZone, Dictionary<Entity, GameObject>>();

    public GameObject GetView(Entity entity, ViewZone zone)
    {
        if (!mapping.ContainsKey(zone))
        {
            Debug.LogError(string.Format("No mapping for zone {0}", zone));
            return null;
        }

        var zoneMap = mapping[zone];
        if (!zoneMap.ContainsKey(entity))
        {
            Debug.LogError(string.Format("No mapping for entity {0} in zone {1}", entity.id, zone));
            return null;
        }

        return zoneMap[entity];
    }

    public void SetView(GameObject view, Entity entity, ViewZone zone)
    {
        if (!mapping.ContainsKey(zone))
            mapping[zone] = new Dictionary<Entity, GameObject>();

        if (view)//view is non-null
        {
            mapping[zone][entity] = view;//set the mapping
            var ev = view.GetComponent<EntityView>();
            if (ev == null)//if GameObject does not have a EV...
                ev = view.AddComponent<EntityView>();
            ev.entity = entity;
        }
        else//view is null
            mapping[zone].Remove(entity);
    }

    private void OnEnable()
    {
        IEntityViewProvider.Register(this);
    }

    private void OnDisable()
    {
        IEntityViewProvider.Reset();
    }
}
public partial struct Entity
{
    public GameObject GetView(ViewZone zone)
    {
        return IEntityViewProvider.Resolve().GetView(this, zone);
    }

    public void SetView(GameObject view, ViewZone zone)
    {
        IEntityViewProvider.Resolve().SetView(view, this, zone);
    }
}