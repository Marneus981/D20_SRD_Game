public class DependencyCollection<T>
{
    public static PriorityList<T> Collection => _collection;

    private static PriorityList<T> _collection = new PriorityList<T>();

    public static void Register(T instance)
    {
        _collection.Add(instance);
    }
}
