public interface ISavingThrowProficiencySystem : IDependency<ISavingThrowProficiencySystem>
{
    Proficiency Get(Entity entity, SavingThrow savingThrow);
    void Set(Entity entity, SavingThrow savingThrow, Proficiency value);
}

public class SavingThrowProficiencySystem : ISavingThrowProficiencySystem
{
    public Proficiency Get(Entity entity, SavingThrow savingThrow)
    {
        return GetSystem(savingThrow).Get(entity);
    }

    public void Set(Entity entity, SavingThrow savingThrow, Proficiency value)
    {
        GetSystem(savingThrow).Set(entity, value);
    }

    IEntityTableSystem<Proficiency> GetSystem(SavingThrow savingThrow)
    {
        switch (savingThrow)
        {
            case SavingThrow.Fortitude:
                return IFortitudeProficiencySystem.Resolve();
            case SavingThrow.Reflex:
                return IReflexProficiencySystem.Resolve();
            case SavingThrow.Will:
                return IWillProficiencySystem.Resolve();
            default:
                return null;
        }
    }
}