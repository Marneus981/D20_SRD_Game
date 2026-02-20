using Cysharp.Threading.Tasks;

public interface IEntryLink
/*
When interacting with an Entry’s “Link”, something will happen. 
It could be immediate, or it could trigger a whole new flow of code. 
Therefore, the interface returns a UniTask, so that we can “await” the completion as needed.
*/
{
    UniTask Select(string link);
}