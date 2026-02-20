using UnityEngine;
using Cysharp.Threading.Tasks;
public interface IEntry
{
    string Text { get; }
    IEntryOption[] Options { get; }
    UniTask SelectLink(string link);
}
public class Entry : MonoBehaviour, IEntry
{
    public string Text { get { return text; } }
    [SerializeField] string text; //Other scripts will only be able to see 
                                //the “Text” via a public readonly property

    public IEntryOption[] Options
    /*
    The “Options” are also a read-only property and merely use a 
    GetComponents on the same GameObject. 
    This means we can attach any Component that conforms to IEntryOption
    and it will automatically be found and used.
    */
    {
        get
        {
            return GetComponents<IEntryOption>();
        }
    }
    public async UniTask SelectLink(string link)
    {
        await GetComponent<IEntryLink>().Select(link);
    }
}