using NUnit.Framework;
using UnityEngine;
using System.Threading.Tasks;

public class EntityRecipeSystemTests
/*
To verify:
    -System gets its asset from the Asset Manager (a mock in this case).
    -System creates a new Entity to configure (via the injected Entity System).
    -System finds all attribute providers attached to the loaded asset and calls 
        Setup on each of them.
    -During Setup, the newly created Entity is passed along as the parameter.
*/
{
    MockAssetManager<GameObject> mockAssetManager;

    [SetUp]
    public void SetUp()
    {
        IDataSystem.Register(new MockDataSystem());
        IDataSystem.Resolve().Create();
        IEntitySystem.Register(new EntitySystem());
        IRandomNumberGenerator.Register(new RandomNumberGenerator());
        mockAssetManager = new MockAssetManager<GameObject>();
        IAssetManager<GameObject>.Register(mockAssetManager);
        //Debug.Log("EntityRecipeSystemTests: Set Up complete");
    }

    [Test]
    public async Task EntityRecipeSystemTestsSimplePasses() //Void before
    {
        // Arrange
        var hero = new GameObject("Hero");
        var provider1 = hero.AddComponent<MockAttributeProvider>();
        var provider2 = hero.AddComponent<MockAttributeProvider>();
        mockAssetManager.fakeAsset = hero;
        var sut = new EntityRecipeSystem();
        //Debug.Log("EntityRecipeSystemTests: Arrange complete");

        // Act
        var entity = await sut.Create("Hero");
        //Debug.Log("EntityRecipeSystemTests: Act complete");

        // Assert
        Assert.IsTrue(provider1.DidSetup);
        Assert.AreEqual(entity, provider1.SetupEntity);
        Assert.IsTrue(provider2.DidSetup);
        Assert.AreEqual(entity, provider2.SetupEntity);
        //Debug.Log("EntityRecipeSystemTests: Assert complete");
    }
}