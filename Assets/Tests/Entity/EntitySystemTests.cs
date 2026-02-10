using NUnit.Framework;
using UnityEngine;

public class EntitySystemTests
{
    MockDataSystem mockDataSystem = new MockDataSystem();
    EntitySystem sut = new EntitySystem();

    [SetUp]
    public void SetUp()
    {
        IDataSystem.Register(mockDataSystem);
        mockDataSystem.Create();
    }

    [TearDown]
    public void TearDown()
    {
        IRandomNumberGenerator.Reset(); //Done so that we have diff values in testing
                                        //We register per test thus
        IDataSystem.Reset();
    }
    [Test]
    public void Create_Succeeds()
    {
        IRandomNumberGenerator.Register(new MockFixedRNG(1));

        var entity = sut.Create();

        Assert.AreEqual(1, entity.id);
        Assert.True(mockDataSystem.Data.entities.Contains(entity));
    }
    [Test]
    public void Create_ZeroId_RollsAgain()
    /*
    This test validates that our system will not return an Entity with an id of 0
    which is reserved to represent a null or unassigned “reference”. 
    When arranging the test, we Register a MockSequenceRNG that generates a 0 first
    (an invalid id) followed by a 1 (a valid id).
    */
    {
        IRandomNumberGenerator.Register(new MockSequenceRNG(0, 1));

        var entity = sut.Create();

        Assert.AreEqual(1, entity.id);
    }
    [Test]
    public void Create_DuplicateId_RollsAgain()
    /*
    This Test validates that our system will not return an Entity with 
    an id that matches an existing Entity in our game Data’s Set of entities. 
    We insert an Entity with an id of 1 into the game Data. Then,
    we register our mock sequence random number generator and configure it
    so that it will generate a 1 first (only invalid because it is already taken)
    and then a 2 (a valid un-used id).
    */
    {
        IRandomNumberGenerator.Register(new MockSequenceRNG(1, 2));
        mockDataSystem.Data.entities.Add(new Entity(1));

        var entity = sut.Create();

        Assert.AreEqual(2, entity.id);
    }
    [Test]
    public void Destroy_Succeeds()
    /*
    Our final test validates that our system can destroy an Entity successfully. 
    This means that our game Data’s Set of entities will no longer contain the
    specified Entity.
    */
    {
        var entity = new Entity(1);
        mockDataSystem.Data.entities.Add(entity);

        sut.Destroy(entity);

        Assert.IsFalse(mockDataSystem.Data.entities.Contains(entity));
    }
}