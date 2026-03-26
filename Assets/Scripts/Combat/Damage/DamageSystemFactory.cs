[DependencyFactory(typeof(IDamageSystem))]
public class DamageSystemFactory : IDependencyFactory<IDamageSystem>
{
    public IDamageSystem Create()
    {
        IDamageSystem system = new DamageSystem();
        system.Add(new DamageTypeSystem("physical", new string[] { "bludgeoning", "piercing", "slashing" }));
        system.Add(new DamageTypeSystem("energy", new string[] { "acid", "cold", "electricity", "fire", "sonic" }));
        system.Add(new DamageTypeSystem("alignment", new string[] { "chaotic", "evil", "good", "lawful" }));
        system.Add(new DamageTypeSystem("mental", new string[0]));
        system.Add(new DamageTypeSystem("poison", new string[0]));
        system.Add(new DamageTypeSystem("bleed", new string[0]));
        system.Add(new DamageTypeSystem("precision", new string[0]));
        system.Add(new MaterialDamageTypeSystem());
        return system;
    }
}