public static class DamageInjector
{
    public static void Inject()
    {
        var damageSystem = new DamageSystem();
        damageSystem.Add(new DamageTypeSystem("physical", new string[] { "bludgeoning", "piercing", "slashing" }));
        damageSystem.Add(new DamageTypeSystem("energy", new string[] { "acid", "cold", "electricity", "fire", "sonic" }));
        damageSystem.Add(new DamageTypeSystem("alignment", new string[] { "chaotic", "evil", "good", "lawful" }));
        damageSystem.Add(new DamageTypeSystem("mental", new string[0]));
        damageSystem.Add(new DamageTypeSystem("poison", new string[0]));
        damageSystem.Add(new DamageTypeSystem("bleed", new string[0]));
        damageSystem.Add(new DamageTypeSystem("precision", new string[0]));
        damageSystem.Add(new MaterialDamageTypeSystem());

        IDamageSystem.Register(damageSystem);
        IDamageImmunitySystem.Register(new DamageImmunitySystem());
        IDamageWeaknessSystem.Register(new DamageWeaknessSystem());
        IDamageResistanceSystem.Register(new DamageResistanceSystem());
        IDamageResistanceExceptionSystem.Register(new DamageResistanceExceptionSystem());
    }
}