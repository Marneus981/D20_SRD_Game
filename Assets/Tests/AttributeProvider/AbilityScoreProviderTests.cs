using NUnit.Framework;
using UnityEngine;

public class AbilityScoreProviderTests
{
    AbilityScoreProvider AddAbilityScore(GameObject asset, AbilityScore.Attribute attribute, int value)
    {
        var provider = asset.AddComponent<AbilityScoreProvider>();
        var json = $"{{\"attribute\":{(int)attribute},\"value\":{value}}}";
        JsonUtility.FromJsonOverwrite(json, provider);
        return provider;
    }
    [SetUp]
    public void SetUp()
    {
        ISetUpSystem.Register(new SetUpSystem());
        ITearDownSystem.Register(new TearDownSystem());
        new DependencyInjection().Init();
        IDataSystem.Register(new MockDataSystem());
        IDataSystem.Resolve().Create();
    }

    [Test]
    public void AbilityScoreProviderTestsSimplePasses()
    {
        // Arrange
        var asset = new GameObject("Hero");
        var provider = asset.AddComponent<AbilityScoreProvider>();
        var json = "{\"attribute\":1,\"value\":12}";
        JsonUtility.FromJsonOverwrite(json, provider);
        var entity = new Entity(123);

        // Act
        provider.Setup(entity);

        // Assert
        Assert.AreEqual(12, entity.Dexterity.value);
    }
    [Test]
    public void AbilityScoreProviderTestsSimplePassesModular()
    {
        // Arrange
        var asset = new GameObject("Hero");
        var provider = AddAbilityScore(asset,AbilityScore.Attribute.Dexterity, 12);
        var entity = new Entity(123);

        // Act
        provider.Setup(entity);

        // Assert
        Assert.AreEqual(12, entity.Dexterity.value);
    }
}