using NUnit.Framework;

public class DamageSystemTests
{
    Entity entity;
    string damageType;
    string material;
    DamageInfo damageInfo;

    [SetUp]
    public void SetUp()
    {
        ISetUpSystem.Register(new SetUpSystem());
        ITearDownSystem.Register(new TearDownSystem());
        DamageInjector.Inject();
        entity = new Entity(123);
        damageType = "slashing";
        material = "silver";
        damageInfo = new DamageInfo
        {
            target = entity,
            damage = 10,
            criticalDamage = 5,
            type = damageType,
            material = material
        };

        IDataSystem.Register(new MockDataSystem());
        IDataSystem.Resolve().Create();
    }
    [Test]
    public void NoImmunityWeaknessResistance_ReturnsInitialForce()
    {
        var sut = IDamageSystem.Resolve();
        var result = sut.Apply(damageInfo);
        Assert.AreEqual(15, result);
    }
    [Test]
    public void DamageTypeImmunity_ReturnsNoDamage()
    {
        IDamageImmunitySystem.Resolve().AddImmunity(entity, damageType);
        var sut = IDamageSystem.Resolve();
        var result = sut.Apply(damageInfo);
        Assert.AreEqual(0, result);
    }
    [Test]
    public void MaterialImmunity_ReturnsNoDamage()
    {
        IDamageImmunitySystem.Resolve().AddImmunity(entity, material);
        var sut = IDamageSystem.Resolve();
        var result = sut.Apply(damageInfo);
        Assert.AreEqual(0, result);
    }
    [Test]
    public void DamageTypeWeakness_ReturnsExtraDamage()
    {
        IDamageWeaknessSystem.Resolve().SetWeakness(entity, damageType, 5);
        var sut = IDamageSystem.Resolve();
        var result = sut.Apply(damageInfo);
        Assert.AreEqual(20, result);
    }
    [Test]
    public void MaterialWeakness_ReturnsExtraDamage()
    {
        IDamageWeaknessSystem.Resolve().SetWeakness(entity, material, 5);
        var sut = IDamageSystem.Resolve();
        var result = sut.Apply(damageInfo);
        Assert.AreEqual(20, result);
    }
    [Test]
    public void DamageTypeResistance_ReturnsLessDamage()
    {
        IDamageResistanceSystem.Resolve().SetResistance(entity, damageType, 5);
        var sut = IDamageSystem.Resolve();
        var result = sut.Apply(damageInfo);
        Assert.AreEqual(10, result);
    }
    [Test]
    public void MaterialResistance_ReturnsLessDamage()
    {
        IDamageResistanceSystem.Resolve().SetResistance(entity, material, 5);
        var sut = IDamageSystem.Resolve();
        var result = sut.Apply(damageInfo);
        Assert.AreEqual(10, result);
    }
    [Test]
    public void DamageTypeResistanceException_ReturnsInitialForce()
    {
        IDamageResistanceExceptionSystem.Resolve().SetException(entity, damageType, material);
        IDamageResistanceSystem.Resolve().SetResistance(entity, damageType, 5);
        var sut = IDamageSystem.Resolve();
        var result = sut.Apply(damageInfo);
        Assert.AreEqual(15, result);
    }
}