public enum AdventureItem
/*
We use an enum bc:
    Simple way to rep something (basically just a named number)
    If we wanted something more complex, we could always use these to
        load assets by the same name – something like the way we handle the Entry.

*/
{
    SkeletonKey,
    Torch
}
public partial class Data
//Simple "Inventory" (for tracking, order does not matter)
{
    public CoreSet<AdventureItem> items = new CoreSet<AdventureItem>(); //Set of items the player has
}
public interface IAdventureItemSystem : IDependency<IAdventureItemSystem>
{
    void Take(AdventureItem item);
    void Drop(AdventureItem item);
    bool Has(AdventureItem item);
}
public class AdventureItemSystem : IAdventureItemSystem
{
    CoreSet<AdventureItem> Items { get { return IDataSystem.Resolve().Data.items; } }

    public void Take(AdventureItem item)
    {
        Items.Add(item);
    }

    public void Drop(AdventureItem item)
    {
        Items.Remove(item);
    }

    public bool Has(AdventureItem item)
    {
        return Items.Contains(item);
    }
}