using NUnit.Framework;
using UnityEngine;
using System.Threading.Tasks;

public class SoloHeroSystemTests
{
    MockEntityRecipeSystem mockEntityRecipeSystem;

    [SetUp]
    public void SetUp()
    {
        ISetUpSystem.Register(new SetUpSystem());
        ITearDownSystem.Register(new TearDownSystem());
        IDataSystem.Register(new MockDataSystem());
        IDataSystem.Resolve().Create();
        AbilityScoreInjector.Inject();
        SkillsInjector.Inject();
        ILevelSystem.Register(new LevelSystem());
        mockEntityRecipeSystem = new MockEntityRecipeSystem();
        IEntityRecipeSystem.Register(mockEntityRecipeSystem);
        //Debug.Log("SoloHeroSystemTests: Set Up complete");
    }

    [Test]
    public async Task SoloHeroSystemTestsSimplePasses() //Void before
    {
        // Arrange
        mockEntityRecipeSystem.fakeEntity = CreateSoloHero();
        var sut = new SoloHeroSystem();
        //Debug.Log("SoloHeroSystemTests: Arrange complete");

        // Act
        await sut.CreateHero();
        //Debug.Log("SoloHeroSystemTests: Act complete");

        // Assert
        Assert.AreEqual(7, sut.Hero.Athletics); // (+4 str, +2 trained, +1 level)
        //Debug.Log("SoloHeroSystemTests: Assert complete");
    }

    Entity CreateSoloHero()
    {
        var result = new Entity(123);
        result.Level = 1;
        result.Strength = 18;
        IProficiencySystem.Resolve().Set(result, Skill.Athletics, Proficiency.Trained);
        //Debug.Log("SoloHeroSystemTests: CreateSoloHero: Assert complete");
        return result;
    }
}