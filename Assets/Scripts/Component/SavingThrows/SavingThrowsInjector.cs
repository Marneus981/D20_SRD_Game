public class SavingThrowsInjector
{
	public static void Inject()
	{
		IFortitudeProficiencySystem.Register(new FortitudeProficiencySystem());
		IFortitudeSystem.Register(new FortitudeSystem());
		IReflexProficiencySystem.Register(new ReflexProficiencySystem());
		IReflexSystem.Register(new ReflexSystem());
		ISavingThrowProficiencySystem.Register(new SavingThrowProficiencySystem());
		ISavingThrowSystem.Register(new SavingThrowSystem());
		IWillProficiencySystem.Register(new WillProficiencySystem());
		IWillSystem.Register(new WillSystem());
	}
}