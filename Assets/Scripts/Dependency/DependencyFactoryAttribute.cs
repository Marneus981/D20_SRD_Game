using System;
using System.Reflection;

public class DependencyFactoryAttribute : InjectableAttribute
{
    private Type _type;

    public DependencyFactoryAttribute(Type type)
    {
        _type = type;
    }

    public override void Inject(object instance)
    {
        Register(Create(instance));
    }

    private object Create(object factory)
    {
        var generic = typeof(IDependencyFactory<>);
        var specific = generic.MakeGenericType(_type);
        MethodInfo method = specific.GetMethod("Create");
        return method.Invoke(factory, null);
    }

    private void Register(object instance)
    {
        var generic = typeof(IDependency<>);
        var specific = generic.MakeGenericType(_type);
        MethodInfo method = specific.GetMethod("Register", new Type[] { _type });
        method.Invoke(null, new[] { instance });
    }
}
