using UnityEngine;

public interface IRandomNumberGenerator : IDependency<IRandomNumberGenerator>
/*
Interface with a Range method matching the signature that Unity had provided. 

Concrete implementation that wraps Unity’s method. Now in any test that relies on a randomly generated number, 
we can Register a mock for unit testing.
*/
{
    public int Range(int minInclusive, int maxExclusive);
}

[Dependency(typeof(IRandomNumberGenerator))]
public struct RandomNumberGenerator : IRandomNumberGenerator
{
    public int Range(int minInclusive, int maxExclusive)
    {
        return Random.Range(minInclusive, maxExclusive);
    }
}