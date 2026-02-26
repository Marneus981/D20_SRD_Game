using UnityEngine;

public class EncounterEntryOption : MonoBehaviour, IEntryOption
{
    public string Text { get { return text; } }
    [SerializeField] string text;
    [SerializeField] string encounterName;

    public void Select()
    {
        IEncounterSystem.Resolve().SetName(encounterName);
    }
}