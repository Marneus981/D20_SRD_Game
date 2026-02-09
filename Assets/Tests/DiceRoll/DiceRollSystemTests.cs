using NUnit.Framework;

public class DiceRollSystemTests
{
    [SetUp]
    //Here we use the SetUp method to Register a mock for our IRandomNumberGenerator interface.
    public void SetUp()
    {
        IRandomNumberGenerator.Register(new MockFixedRNG(7));
    }

    [TearDown]
    //Clears the registration when test is done
    public void TearDown()
    {
        IRandomNumberGenerator.Reset();
    }

    [Test]
    public void Roll_Passes()
    /*
    Creates the sut (subject under test) as well as a sample DiceRoll model,
    then uses the system to roll the dice roll and match it against the expected result (18).
    */
    {
        var sut = new DiceRollSystem();
        var diceRoll = new DiceRoll(2, 10, 4);
        Assert.AreEqual(18, sut.Roll(diceRoll));
    }
}
