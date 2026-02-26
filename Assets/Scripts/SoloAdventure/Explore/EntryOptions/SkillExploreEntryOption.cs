using UnityEngine;

public class SkillExploreEntryOption : MonoBehaviour, IEntryOption
{
    [SerializeField] string text;
    [SerializeField] Skill skill;
    [SerializeField] int difficultyCheck;
    [SerializeField] string criticalFailureEntry;
    [SerializeField] string failureEntry;
    [SerializeField] string successEntry;
    [SerializeField] string criticalSuccessEntry;

    public string Text
    {
        get { return text; }
    }

    public void Select()
    {
        var hero = ISoloHeroSystem.Resolve().Hero;
        var modifier = hero[skill];
        var result = ICheckSystem.Resolve().GetResult(modifier, difficultyCheck);
        var entry = GetEntry(result);
        IEntrySystem.Resolve().SetName(entry);
    }
    string GetEntry(Check result)
    {
        switch (result)
        {
            case Check.CriticalFailure:
                return criticalFailureEntry;
            case Check.Failure:
                return failureEntry;
            case Check.Success:
                return successEntry;
            default: // case Check.CriticalSuccess:
                return criticalSuccessEntry;
        }
    }
}