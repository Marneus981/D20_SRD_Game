public static class ComponentInjector
//Handles any systems at its own hierarchy level and below, 
//and for those below, it relies on the subfolder level injector.
{
    public static void Inject()
    {
        AbilityScoreInjector.Inject();
        INameSystem.Register(new NameSystem());
    }
}