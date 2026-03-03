public static class EntityInjector
{
    public static void Inject()
    {
        IEntitySystem.Register(new EntitySystem());
        IEntityRecipeSystem.Register(new EntityRecipeSystem());
    }
    public static void SetUp()
    {
        IEntitySystem.Resolve().SetUp();
        IEntityRecipeSystem.Resolve().SetUp();
    }

    public static void TearDown()
    {
        IEntitySystem.Resolve().TearDown();
        IEntityRecipeSystem.Resolve().TearDown();
    }
}