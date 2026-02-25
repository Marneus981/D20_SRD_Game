public static class EntityInjector
{
    public static void Inject()
    {
        IEntitySystem.Register(new EntitySystem());
        IEntityRecipeSystem.Register(new EntityRecipeSystem());
    }
}