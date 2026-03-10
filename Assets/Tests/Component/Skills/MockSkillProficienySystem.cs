using System.Collections.Generic;

public class MockSkillProficienySystem : ISkillProficiencySystem
/*
Set any skill proficiency without needing to inject the generic proficiency system
along with the various systems per skill type. It simply holds an in-memory collection
of the mapping from Entity to Skill to Proficiency in that skill.
*/
{
    Dictionary<Entity, Dictionary<Skill, Proficiency>> fakeTable = new Dictionary<Entity, Dictionary<Skill, Proficiency>>();

    public Proficiency Get(Entity entity, Skill skill)
    {
        if (fakeTable.ContainsKey(entity))
        {
            var map = fakeTable[entity];
            if (map.ContainsKey(skill))
                return map[skill];
        }
        return 0;
    }

    public void Set(Entity entity, Skill skill, Proficiency value)
    {
        if (!fakeTable.ContainsKey(entity))
            fakeTable[entity] = new Dictionary<Skill, Proficiency>();

        fakeTable[entity][skill] = value;
    }
}