public static class AbilityScoreInjector
/*
This injector class will have no purpose beyond the initial step of 
registering “things” – each to their relevant interface.
This is a static class, with a static method. 
You can’t create instances of static classes, 
and you use static methods through the class itself. 
The injector class registers each of the systems in the folder. 
They are listed alphabetically,
*/
{
    public static void Inject()
    {
        IAbilityScoreSystem.Register(new AbilityScoreSystem());
        ICharismaSystem.Register(new CharismaSystem());
        IConstitutionSystem.Register(new ConstitutionSystem());
        IDexteritySystem.Register(new DexteritySystem());
        IIntelligenceSystem.Register(new IntelligenceSystem());
        IStrengthSystem.Register(new StrengthSystem());
        IWisdomSystem.Register(new WisdomSystem());
    }
}