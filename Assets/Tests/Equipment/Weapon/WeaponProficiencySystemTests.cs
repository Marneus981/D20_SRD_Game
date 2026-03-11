using NUnit.Framework;

public class WeaponProficiencySystemTests
{
    WeaponProficiencySystem sut;
    Entity hero;
    Entity weapon;

    [SetUp]
    public void SetUp()
    {
        IWeaponCategorySystem.Register(new WeaponCategorySystem());
        IWeaponFilterSystem.Register(new WeaponFilterSystem());
        IWeaponGroupSystem.Register(new WeaponGroupSystem());
        sut = new WeaponProficiencySystem();
        IDataSystem.Register(new MockDataSystem());
        IDataSystem.Resolve().Create();
        hero = new Entity(123);
        weapon = new Entity(456);
        weapon.WeaponCategory = WeaponCategory.Simple;
        weapon.WeaponGroup = WeaponGroup.Sword;
    }

    [TearDown]
    public void TearDown()
    {
        IWeaponCategorySystem.Reset();
        IWeaponFilterSystem.Reset();
        IWeaponGroupSystem.Reset();
        IDataSystem.Reset();
    }

    WeaponTraining CreateTraining(WeaponCategory category, WeaponGroup group, Proficiency proficiency)
    {
        var result = new WeaponTraining();
        result.filter.category = category;
        result.filter.group = group;
        result.proficiency = proficiency;
        return result;
    }
    [Test]
    public void TestDefaultProficiency()
    {
        Assert.AreEqual(sut.GetProficiency(hero, weapon), Proficiency.Untrained);
    }
    [Test]
    public void TestAddWeaponTraining()
    {
        // Arrange
        var training = CreateTraining(WeaponCategory.Simple, WeaponGroup.None, Proficiency.Trained);

        // Act
        sut.AddWeaponTraining(training, hero);

        // Assert
        Assert.IsTrue(sut.Has(hero));
        Assert.AreEqual(sut.GetProficiency(hero, weapon), Proficiency.Trained);
    }
    [Test]
    public void TestGetHighestTrainingProficiency()
    {
        // Arrange
        var simpleTraining = CreateTraining(WeaponCategory.Simple, WeaponGroup.None, Proficiency.Trained);
        var expertTraining = CreateTraining(WeaponCategory.None, WeaponGroup.Sword, Proficiency.Expert);

        // Act
        sut.AddWeaponTraining(simpleTraining, hero);
        sut.AddWeaponTraining(expertTraining, hero);

        // Assert
        //Always return the “highest” matching proficiency for a given weapon
        Assert.AreEqual(sut.GetProficiency(hero, weapon), Proficiency.Expert);
    }
}