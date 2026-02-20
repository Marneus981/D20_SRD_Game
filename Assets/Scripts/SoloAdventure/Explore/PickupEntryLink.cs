using UnityEngine;
using Cysharp.Threading.Tasks;

public class PickupEntryLink : MonoBehaviour, IEntryLink
{
    [SerializeField] AdventureItem item;
    [SerializeField] string takeMessage;
    [SerializeField] string dropMessage;

    public async UniTask Select(string link)
    {
        if (link != "pickup")
            return;

        var system = IAdventureItemSystem.Resolve();//Grab a reference to our new IAdventureItemSystem
        if (!system.Has(item))
        {
            system.Take(item);
            await IAlert.Resolve().Show(takeMessage);
        }
        else
        {
            system.Drop(item);
            await IAlert.Resolve().Show(dropMessage);
        }
    }
}