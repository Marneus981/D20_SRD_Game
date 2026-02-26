public static class FlowInjector
/*
Note that for the FlowInjector, we did not add the AppFlow. 
Even “if” we made it conform to an injectable interface, 
it is implemented as a MonoBehaviour and so would be able to inject itself 
just like the MainMenu script does.
*/
{
    public static void Inject()
    {
        IEncounterFlow.Register(new EncounterFlow());
        IEntryFlow.Register(new EntryFlow());
        IGameFlow.Register(new GameFlow());
        IMainMenuFlow.Register(new MainMenuFlow());
    }
}