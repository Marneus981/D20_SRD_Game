public abstract class EntityRelationTableSystem : EntityTableSystem<Entity>
//In order to handle the idea of a cascading deletion, we “override” the OnEntityDestroyed.
//Whenever we find a relationship based on the destroyed Entity, we now will also destroy the Entity it was related to.
//We also remove the data from the table by calling the superclass implementation.
{
    protected override void OnEntityDestroyed(Entity entity)
    {
        Entity target;
        if (TryGetValue(entity, out target))
        {
            IEntitySystem.Resolve().Destroy(target);
        }
        base.OnEntityDestroyed(entity);
    }
}