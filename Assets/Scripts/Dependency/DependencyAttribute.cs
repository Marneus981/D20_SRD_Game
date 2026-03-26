using System;
using System.Reflection;

public class DependencyAttribute : InjectableAttribute
{
    private Type _type;

    public DependencyAttribute(Type type)
    {
        _type = type;
    }

    public override void Inject(object instance)
    {
        var generic = typeof(IDependency<>);
        var specific = generic.MakeGenericType(_type);
        MethodInfo method = specific.GetMethod("Register", new Type[] { _type });
        method.Invoke(null, new[] { instance });
    }
}