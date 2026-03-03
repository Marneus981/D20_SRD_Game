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
        IHeroActionFlow.Register(new HeroActionFlow());
        IMainMenuFlow.Register(new MainMenuFlow());
        ICombatFlow.Register(new CombatFlow());
        IRoundFlow.Register(new RoundFlow());
        ITurnFlow.Register(new TurnFlow());
        IMonsterActionFlow.Register(new MonsterActionFlow());
    }
    public static void SetUp()
    {
        ICombatFlow.Resolve().SetUp();
        IEncounterFlow.Resolve().SetUp();
        IEntryFlow.Resolve().SetUp();
        IGameFlow.Resolve().SetUp();
        IHeroActionFlow.Resolve().SetUp();
        IMainMenuFlow.Resolve().SetUp();
        IMonsterActionFlow.Resolve().SetUp();
        IRoundFlow.Resolve().SetUp();
        ITurnFlow.Resolve().SetUp();
    }

    public static void TearDown()
    {
        ICombatFlow.Resolve().TearDown();
        IEncounterFlow.Resolve().TearDown();
        IEntryFlow.Resolve().TearDown();
        IGameFlow.Resolve().TearDown();
        IHeroActionFlow.Resolve().TearDown();
        IMainMenuFlow.Resolve().TearDown();
        IMonsterActionFlow.Resolve().TearDown();
        IRoundFlow.Resolve().TearDown();
        ITurnFlow.Resolve().TearDown();
    }
}