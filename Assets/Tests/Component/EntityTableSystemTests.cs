using NUnit.Framework;

class TestEntityTableSystem : EntityTableSystem<int>
/*
Note 1: EntityTableSystem is an abstract class, so we can’t instantiate one directly.
We could test directly with NameSystem but:
    It is possible that the requirements of any given system could change over time.
        e.g.We could imagine needing to override the inherited methods in a scenario
        where we may want to prevent setting a name that is included in a list of bad words.
*/
{
    public override CoreDictionary<Entity, int> Table { get { return _table; } }
    CoreDictionary<Entity, int> _table = new CoreDictionary<Entity, int>();
}
public class EntityTableSystemTests
{
    TestEntityTableSystem sut;
    Entity entity = new Entity(1);
    int value = 123;

    [SetUp]
    public void SetUp()
    {
        ISetUpSystem.Register(new SetUpSystem());
        ITearDownSystem.Register(new TearDownSystem());
        sut = new TestEntityTableSystem();
    }
    //All of the basic CRUD operations were given unit tests:
    [Test]
    public void Set_AddNewValue_Success()
    {
        sut.Set(entity, value);
        Assert.AreEqual(value, sut.Table[entity]);
    }

    [Test]
    public void Set_UpdateValue_Success()
    {
        sut.Table[entity] = 456;
        sut.Set(entity, value);
        Assert.AreEqual(value, sut.Table[entity]);
    }

    [Test]
    public void Get_HasValue_Success()
    {
        sut.Table[entity] = value;
        var result = sut.Get(entity);
        Assert.AreEqual(value, result);
    }

    [Test]
    public void Get_NoValue_ReturnsDefaultValue()
    {
        var result = sut.Get(entity);
        Assert.AreEqual(0, result);
    }

    [Test]
    public void TryGetValue_HasValue_ReturnsTrueAndValue()
    {
        sut.Table[entity] = value;
        int output;
        var result = sut.TryGetValue(entity, out output);
        Assert.AreEqual(value, output);
        Assert.True(result);
    }

    [Test]
    public void TryGetValue_NoValue_ReturnsFalseDefaultValue()
    {
        int output;
        var result = sut.TryGetValue(entity, out output);
        Assert.AreEqual(0, output);
        Assert.False(result);
    }

    [Test]
    public void Has_WithValue_ReturnsTrue()
    {
        sut.Table[entity] = value;
        var result = sut.Has(entity);
        Assert.True(result);
    }

    [Test]
    public void Has_NoValue_ReturnsFalse()
    {
        var result = sut.Has(entity);
        Assert.False(result);
    }

    [Test]
    public void Remove_Success()
    {
        sut.Table[entity] = value;
        sut.Remove(entity);
        Assert.False(sut.Table.ContainsKey(entity));
    }
}