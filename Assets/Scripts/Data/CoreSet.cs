/*
Serializable HashSet
To be used to store unique game Entities
*/
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

[System.Serializable]
public class CoreSet<T> : ISerializationCallbackReceiver, 
                        ICollection<T>, 
                        IEnumerable<T>, 
                        IEnumerable, 
                        IReadOnlyCollection<T>, 
                        ISet<T>, 
                        IDeserializationCallback, 
                        ISerializable
/*
Interfaces can be cathegorized in two:

    ISerializationCallbackReceiver: 
        By Unity; We conform to this interface to be compatible with JsonUtility:
            Gives op to take action before and after serialization. 

    The Rest:
        These are the same a native HashSet conforms to:
            (HashSet Declaration)  public class HashSet<T> : ICollection<T>, 
                IEnumerable<T>, IEnumerable, IReadOnlyCollection<T>, 
                ISet<T>, IDeserializationCallback, ISerializable
*/
{
    /*
    We make use of two collections: 
        Work with the HashSet; Unity can automatically save and load a List.
    */ 
    [SerializeField] List<T> values = new List<T>();
    HashSet<T> set = new HashSet<T>();

    /*
    ISerializationCallbackReceiver Implementation:
        OnBeforeSerialize: Make a List from our Set when we need to save.
        OnAfterDeserialize: Make a Set from our List when we need to load.
    */
    public void OnAfterDeserialize()
    {
        set.Clear();

        var count = values.Count;
        for (int i = 0; i < count; ++i)
            set.Add(values[i]);
    }

    public void OnBeforeSerialize()
    {
        values.Clear();

        foreach (var value in set)
            values.Add(value);
    }

    /*
    The Rest Implementation:
    We "have" a HashSet, thus we only need wrap its implementation
    */
    public int Count => set.Count;
    public bool IsReadOnly => ((ICollection<T>)set).IsReadOnly;
    public void Add(T item) => set.Add(item);
    public void Clear() => set.Clear();
    public bool Contains(T item) => set.Contains(item);
    public void CopyTo(T[] array, int arrayIndex) => set.CopyTo(array, arrayIndex);
    public IEnumerator<T> GetEnumerator() => set.GetEnumerator();
    public bool Remove(T item) => set.Remove(item);
    IEnumerator IEnumerable.GetEnumerator() => ((ICollection<T>) set).GetEnumerator();

    bool ISet<T>.Add(T item) => set.Add(item);
    public void ExceptWith(IEnumerable<T> other) => set.ExceptWith(other);
    public void IntersectWith(IEnumerable<T> other) => set.IntersectWith(other);
    public bool IsProperSubsetOf(IEnumerable<T> other) => set.IsProperSubsetOf(other);
    public bool IsProperSupersetOf(IEnumerable<T> other) => set.IsProperSupersetOf(other);
    public bool IsSubsetOf(IEnumerable<T> other) => set.IsSubsetOf(other);
    public bool IsSupersetOf(IEnumerable<T> other) => set.IsSupersetOf(other);
    public bool Overlaps(IEnumerable<T> other) => set.Overlaps(other);
    public bool SetEquals(IEnumerable<T> other) => set.SetEquals(other);
    public void SymmetricExceptWith(IEnumerable<T> other) => set.SymmetricExceptWith(other);
    public void UnionWith(IEnumerable<T> other) => set.UnionWith(other);

    public void OnDeserialization(object sender) => set.OnDeserialization(sender);

    public void GetObjectData(SerializationInfo info, StreamingContext context) => set.GetObjectData(info, context);
}