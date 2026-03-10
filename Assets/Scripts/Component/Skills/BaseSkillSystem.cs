public interface IBaseSkillSystem : IEntityTableSystem<int>
/*
Inherits from IEntityTableSystem bC primary purpose of the system is to map from an Entity to an int value
representing its “Skill” level
*/
{
    void Setup(Entity entity); //To calculate skill value of an entity (on creation/levelup)
}

public abstract class BaseSkillSystem : EntityTableSystem<int>, IBaseSkillSystem
{
    //Protected bc: Subclasses may wish to work with those properties, 
    //but any other external class has no reason to know about that data.
    protected abstract Skill Skill { get; } //enum type that the system deals with
    protected abstract AbilityScore.Attribute Attribute { get; }//type of AbilityScore most directly responsible for skill

    public virtual void Setup(Entity entity)
    //“virtual”: sample implementation of the method but that it can still be overwritten for special handling.
    //“protected”: this method may be used by the subclasses, but should not be used by anything else.
    {
        Table[entity] = Calculate(entity);
    }

    protected virtual int Calculate(Entity entity)
    {
        int result = entity[Attribute].Modifier;
        var proficiency = ISkillProficiencySystem.Resolve().Get(entity, Skill);
        if (proficiency != Proficiency.Untrained)
            result += (int)proficiency * 2 + entity.Level;
        return result;
    }
}