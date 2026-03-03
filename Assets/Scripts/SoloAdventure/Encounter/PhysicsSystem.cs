using UnityEngine;

public interface IPhysicsSystem : IDependency<IPhysicsSystem>
{
    Entity? OverlapPoint(Point point, int layerMask);//Check entities at point
                                                     //layerMask is used to cast against
}

public class PhysicsSystem : IPhysicsSystem
{
    const int maxResultCount = 10;
    Collider2D[] results = new Collider2D[maxResultCount];//reusable colider array

    public Entity? OverlapPoint(Point point, int layerMask)
    {
        Vector2 pos = new Vector2(point.x + 0.5f, point.y + 0.5f);//check middle os entity square
        var resultCount = Physics2D.OverlapPointNonAlloc(pos, results, layerMask);
        for (int i = 0; i < Mathf.Min(resultCount, maxResultCount); ++i)
        {
            var entityView = results[i].GetComponent<EntityView>();
            if (entityView)
                return entityView.entity;
        }
        return null;
    }
}