using System.Diagnostics;
using UnityEngine;

public static class ArrayExtensions
{
    public static T Random<T>(this T[] array)
    //TODO: handle hull and empty arrays better
    {
        if (array == null){
            UnityEngine.Debug.Log("Array Extensions: Random: null array");
            return default;
        }
        var lenght = array.Length;
        if (lenght == 0)
        {
            UnityEngine.Debug.Log("Array Extensions: Random: 0 lenght array");
            return default;
        }
        var index = UnityEngine.Random.Range(0, lenght);
        return array[index];
    }
}