using System.Collections.Generic;
using UnityEngine;

public class RandomBackgroundProvider : MonoBehaviour, IAttributeProvider
{
    public List<string> names;

    public void Setup(Entity entity)
    {
        // TODO: If names is empty, pick from a common list
        if (names == null || names.Count == 0)
        {
            Debug.LogError("No background assigned");
            return;
        }

        var rnd = IRandomNumberGenerator.Resolve().Range(0, names.Count);
        entity.Background = names[rnd];
    }
}