public class MockBaseSkillSystem : BaseSkillSystem
/*
Simulate a concrete subclass such as the “AthleticsSkillSystem”. 
It lets you specify the type of skill and attribute that are relevant, 
but does not override any of the other base class functionality such as the “Calculate” method. 
This guarantees that we can test the “base” class functionality without any potential overriding
that other subclasses may require.
*/
{
    CoreDictionary<Entity, int> fakeTable = new CoreDictionary<Entity, int>();
    Skill fakeSkill;
    AbilityScore.Attribute fakeAttribute;

    public MockBaseSkillSystem(Skill fakeSkill, AbilityScore.Attribute fakeAttribute)
    {
        this.fakeSkill = fakeSkill;
        this.fakeAttribute = fakeAttribute;
    }

    public override CoreDictionary<Entity, int> Table => fakeTable;
    protected override Skill Skill => fakeSkill;
    protected override AbilityScore.Attribute Attribute => fakeAttribute;
}