using UnityEngine;
using System.Collections.Generic;

public class MoveToNearestTarget : MonoBehaviour, IStridePositionSelector
{
    [SerializeField] EntityFilter filter;

    public Point SelectPosition(Entity entity, IPathMap map)
    {
        // Step 1: Fetch a list of all valid targets
        var targets = filter.Fetch(entity);

        // Step 2: Grab the adjacent tiles for each target
        var adjacentSpaces = new HashSet<Point>();
        foreach (var target in targets)
        {
            var spaces = ISpaceSystem.Resolve().AdjacentSpaces(entity, target);
            adjacentSpaces.UnionWith(spaces);
        }

        // Step 3: Filter the tiles to only include "open" positions
        var positions = new HashSet<Point>(adjacentSpaces);
        var openPositions = map.OpenPoints(int.MaxValue);
        positions.IntersectWith(openPositions);

        // Step 4: If none of the adjacent positions are "open"
        // fallback to any "known" position
        if (positions.Count == 0)
        {
            positions.UnionWith(adjacentSpaces);
            positions.IntersectWith(map.AllPoints());
        }

        // Step 5: Pick the nearest position
        var bestPosition = entity.Position;
        var bestCost = int.MaxValue;
        foreach (var position in positions)
        {
            var cost = map[position].moveCost;
            if (cost < bestCost)
            {
                bestPosition = position;
                bestCost = cost;
            }
        }

        // Step 6: Return a tile within range of the entity's speed
        var destination = map.NearestOpen(bestPosition, entity.Speed);
        return destination;
    }
}