using System;
//Wrapper for systems that handle combining proficiency with an ability score modifier
public interface ISavingThrowSystem : IDependency<ISavingThrowSystem>
{
    void Set(Entity entity, SavingThrow savingThrow, int value);
    int Get(Entity entity, SavingThrow savingThrow);
    void SetupAllSavingThrows(Entity entity);
    void Setup(Entity entity, SavingThrow savingThrow);
}

public class SavingThrowSystem : ISavingThrowSystem
{
    public void Set(Entity entity, SavingThrow savingThrow, int value)
    {
        GetSystem(savingThrow).Set(entity, value);
    }

    public int Get(Entity entity, SavingThrow savingThrow)
    {
        return GetSystem(savingThrow).Get(entity);
    }

    public void SetupAllSavingThrows(Entity entity)
    {
        foreach (SavingThrow savingThrow in Enum.GetValues(typeof(SavingThrow)))
            GetSystem(savingThrow).Setup(entity);
    }

    public void Setup(Entity entity, SavingThrow savingThrow)
    {
        GetSystem(savingThrow).Setup(entity);
    }

    IBaseSavingThrowSystem GetSystem(SavingThrow savingThrow)
    {
        switch (savingThrow)
        {
            case SavingThrow.Fortitude:
                return IFortitudeSystem.Resolve();
            case SavingThrow.Reflex:
                return IReflexSystem.Resolve();
            case SavingThrow.Will:
                return IWillSystem.Resolve();
            default:
                return null;
        }
    }
}