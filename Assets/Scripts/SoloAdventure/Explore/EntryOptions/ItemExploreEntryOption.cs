using UnityEngine;

public class ItemExploreEntryOption : MonoBehaviour, IEntryOption
//To make dynamic entries dependent on item possession
{
    [SerializeField] string text;//label to show on the button for the option
    [SerializeField] AdventureItem item;//adventure item to base the navigation requirement on
    [SerializeField] string hasItemEntry;//name of an entry asset to navigate to if the user has the item
    [SerializeField] string noItemEntry;//name of an entry asset to navigate to if the user does not have the item

    public string Text
    //read only access; part of conformance of the IEntryOption interface.
    {
        get { return text; }
    }

    public void Select()
    {
        var entry = IAdventureItemSystem.Resolve().Has(item) ? hasItemEntry : noItemEntry;
        IEntrySystem.Resolve().SetName(entry);
    }
}