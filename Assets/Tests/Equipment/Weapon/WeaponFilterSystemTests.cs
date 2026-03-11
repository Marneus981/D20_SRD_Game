using NUnit.Framework;

public class WeaponFilterSystemTests
{
    const string weaponName = "Sword";
    WeaponFilterSystem sut;
    Entity weapon;
    [SetUp]
    public void SetUp()
    {
        INameSystem.Register(new NameSystem());
        IWeaponCategorySystem.Register(new WeaponCategorySystem());
        IWeaponGroupSystem.Register(new WeaponGroupSystem());
        sut = new WeaponFilterSystem();
        IDataSystem.Register(new MockDataSystem());
        IDataSystem.Resolve().Create();
        weapon = CreateSimpleSword();
    }

    [TearDown]
    public void TearDown()
    {
        INameSystem.Reset();
        IWeaponCategorySystem.Reset();
        IWeaponGroupSystem.Reset();
        IDataSystem.Reset();
    }
    Entity CreateSimpleSword()
    {
        var weapon = new Entity(123);
        weapon.Name = weaponName;
        weapon.WeaponCategory = WeaponCategory.Simple;
        weapon.WeaponGroup = WeaponGroup.Sword;
        return weapon;
    }
    [Test]
    public void TestMatchingName()
    {
        // Arrange
        var filter = new WeaponFilter();
        filter.name = weaponName;

        // Act
        var result = sut.Matches(filter, weapon);

        // Assert
        Assert.IsTrue(result);
    }

    [Test]
    public void TestNonMatchingName()
    {
        // Arrange
        var filter = new WeaponFilter();
        filter.name = "Foo";

        // Act
        var result = sut.Matches(filter, weapon);

        // Assert
        Assert.IsFalse(result);
    }
    [Test]
    public void TestMatchingCategory()
    {
        // Arrange
        var filter = new WeaponFilter();
        filter.category = WeaponCategory.Simple;

        // Act
        var result = sut.Matches(filter, weapon);

        // Assert
        Assert.IsTrue(result);
    }

    [Test]
    public void TestNonMatchingCategory()
    {
        // Arrange
        var filter = new WeaponFilter();
        filter.category = WeaponCategory.Advanced;

        // Act
        var result = sut.Matches(filter, weapon);

        // Assert
        Assert.IsFalse(result);
    }
    [Test]
    public void TestMatchingGroup()
    {
        // Arrange
        var filter = new WeaponFilter();
        filter.group = WeaponGroup.Sword;

        // Act
        var result = sut.Matches(filter, weapon);

        // Assert
        Assert.IsTrue(result);
    }

    [Test]
    public void TestNonMatchingGroup()
    {
        // Arrange
        var filter = new WeaponFilter();
        filter.group = WeaponGroup.Axe;

        // Act
        var result = sut.Matches(filter, weapon);

        // Assert
        Assert.IsFalse(result);
    }
    [Test]
    public void TestMatchingFromMultipleOptions()
    {
        // Arrange
        var filter = new WeaponFilter();
        filter.category = WeaponCategory.Advanced | WeaponCategory.Simple;
        filter.group = WeaponGroup.Axe | WeaponGroup.Bow | WeaponGroup.Sword;

        // Act
        var result = sut.Matches(filter, weapon);

        // Assert
        Assert.IsTrue(result);
    }

    [Test]
    public void TestNonMatchingFromMultipleOptions()
    {
        // Arrange
        var filter = new WeaponFilter();
        filter.category = WeaponCategory.Advanced | WeaponCategory.Martial;
        filter.group = WeaponGroup.Axe | WeaponGroup.Bow | WeaponGroup.Club;

        // Act
        var result = sut.Matches(filter, weapon);

        // Assert
        Assert.IsFalse(result);
    }
}