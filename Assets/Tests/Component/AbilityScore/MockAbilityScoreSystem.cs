using System.Collections.Generic;
using NUnit.Framework;

public class MockAbilityScoreSystem : IAbilityScoreSystem
/*
Set any ability score without needing to inject the generic ability score system and
the various systems per attribute. It simply holds an in-memory collection of the mapping
from Entity to Attribute type to Ability Score value. 
*/
{
    Dictionary<Entity, Dictionary<AbilityScore.Attribute, AbilityScore>> fakeTable = new Dictionary<Entity, Dictionary<AbilityScore.Attribute, AbilityScore>>();

    public AbilityScore Get(Entity entity, AbilityScore.Attribute attribute)
    {
        if (fakeTable.ContainsKey(entity))
        {
            var map = fakeTable[entity];
            if (map.ContainsKey(attribute))
                return map[attribute];
        }
        return new AbilityScore(0);
    }

    public void Set(Entity entity, AbilityScore.Attribute attribute, AbilityScore value)
    {
        if (!fakeTable.ContainsKey(entity))
            fakeTable[entity] = new Dictionary<AbilityScore.Attribute, AbilityScore>();

        fakeTable[entity][attribute] = value;
    }

    public void Set(Entity entity, IEnumerable<int> scores)
    {
        Assert.Fail("Using un-implemented mock feature");
    }
}