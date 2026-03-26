using System;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
public abstract class InjectableAttribute : Attribute
{
    public abstract void Inject(object instance);
}
