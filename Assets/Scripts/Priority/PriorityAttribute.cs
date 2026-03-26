using System;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public abstract class PriorityAttribute : Attribute
{
    public abstract int GetPriority();
}

public class PriorityFixedAttribute : PriorityAttribute
{
    private int _value;

    public PriorityFixedAttribute(int value)
    {
        _value = value;
    }

    public override int GetPriority()
    {
        return _value;
    }
}

public class PriorityBeforeAttribute : PriorityAttribute
{
    private System.Type _type;

    public PriorityBeforeAttribute(System.Type type)
    {
        _type = type;
    }

    public override int GetPriority()
    {
        return _type.GetPriority() - 1;
    }
}

public class PriorityAfterAttribute : PriorityAttribute
{
    private System.Type _type;

    public PriorityAfterAttribute(System.Type type)
    {
        _type = type;
    }

    public override int GetPriority()
    {
        return _type.GetPriority() + 1;
    }
}

public static class PriorityTypeExtensions
{
    public static int GetPriority(this System.Type type)
    {
        foreach (var attribute in type.GetCustomAttributes(true))
        {
            if (attribute is PriorityAttribute priorityAttribute)
            {
                return priorityAttribute.GetPriority();
            }
        }
        return Priority.Normal;
    }
}
