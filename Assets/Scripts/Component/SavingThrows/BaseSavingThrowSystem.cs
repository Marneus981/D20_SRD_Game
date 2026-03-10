public interface IBaseSavingThrowSystem : IEntityTableSystem<int>
{
    void Setup(Entity entity);
}

public abstract class BaseSavingThrowSystem : EntityTableSystem<int>, IBaseSavingThrowSystem
/*
Base class is abstract: we can’t create instances of it. 
It must be subclassed in order to be used, and each subclass 
will have to provide some of the needed information. 
In particular, each subclass will let the base class know which
type of SavingThrow it is for, and what type of AbilityScore it uses
in its calculations.
 */
{
    protected abstract SavingThrow SavingThrow { get; }
    protected abstract AbilityScore.Attribute Attribute { get; }

    public virtual void Setup(Entity entity)
    {
        Table[entity] = Calculate(entity);
    }

    protected virtual int Calculate(Entity entity)
    {
        int result = entity[Attribute].Modifier;
        var proficiency = ISavingThrowProficiencySystem.Resolve().Get(entity, SavingThrow);
        if (proficiency != Proficiency.Untrained)
            result += (int)proficiency * 2 + entity.Level;
        UnityEngine.Debug.Log(string.Format("SavingThrow: {0}, Value: {1}", SavingThrow.ToString(), result));
        return result;
    }
}