public static class DiceRollInjector
{
    public static void Inject()
    {
        IDiceRollSystem.Register(new DiceRollSystem());
        IRandomNumberGenerator.Register(new RandomNumberGenerator());
    }
    public static void SetUp()
    {
        IDiceRollSystem.Resolve().SetUp();
        IRandomNumberGenerator.Resolve().SetUp();
    }

    public static void TearDown()
    {
        IDiceRollSystem.Resolve().TearDown();
        IRandomNumberGenerator.Resolve().TearDown();
    }
}