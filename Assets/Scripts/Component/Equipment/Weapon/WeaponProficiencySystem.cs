using System;
using System.Collections.Generic;

[Serializable]
public class WeaponTraining
{
    public WeaponFilter filter = new WeaponFilter();
    public Proficiency proficiency;
}

[Serializable]
public class WeaponProficiency
{
    public List<WeaponTraining> training = new List<WeaponTraining>();
}

public partial class Data
{
    public CoreDictionary<Entity, WeaponProficiency> weaponProficiency = new CoreDictionary<Entity, WeaponProficiency>();
}

public interface IWeaponProficiencySystem : IDependency<IWeaponProficiencySystem>, IEntityTableSystem<WeaponProficiency>
{
    void AddWeaponTraining(WeaponTraining training, Entity entity);
    Proficiency GetProficiency(Entity entity, Entity weapon);
}
[Dependency(typeof(IWeaponProficiencySystem))]
public class WeaponProficiencySystem : EntityTableSystem<WeaponProficiency>, IWeaponProficiencySystem
{
    public override CoreDictionary<Entity, WeaponProficiency> Table => IDataSystem.Resolve().Data.weaponProficiency;

    public void AddWeaponTraining(WeaponTraining training, Entity entity)
    {
        if (!Has(entity))
            Set(entity, new WeaponProficiency());
        var value = Get(entity);
        value.training.Add(training);
    }

    public Proficiency GetProficiency(Entity entity, Entity weapon)
    {
        var result = Proficiency.Untrained;
        if (!Has(entity))
            return result;

        var allTraining = Get(entity).training;
        foreach (var training in Get(entity).training)
        {
            var prof = GetProficiency(weapon, training);
            if (prof > result)
                result = prof;
        }
        return result;
    }

    Proficiency GetProficiency(Entity weapon, WeaponTraining training)
    {
        if (IWeaponFilterSystem.Resolve().Matches(training.filter, weapon))
            return training.proficiency;
        return Proficiency.Untrained;
    }
}