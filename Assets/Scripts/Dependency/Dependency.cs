public class Dependency<T>
{
    static T _instance;

    public static void Register(T instance)
    {
        _instance = instance;
    }

    public static void Reset()
    {
        _instance = default;
    }

    public static T Resolve()
    {
        return _instance;
    }
}