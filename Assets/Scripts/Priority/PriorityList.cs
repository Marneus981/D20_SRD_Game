using System.Collections.Generic;
using System.Collections;

public class PriorityList<T> : IEnumerable<T>, IEnumerable
{
    private List<T> _list = new();
    private bool _isDirty;

    public void Add(T item)
    {
        _list.Add(item);
        _isDirty = true;
    }

    public void Remove(T item)
    {
        _list.Remove(item);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        Sort();
        return (_list as IEnumerable).GetEnumerator();
    }

    IEnumerator<T> IEnumerable<T>.GetEnumerator()
    {
        Sort();
        return (_list as IEnumerable<T>).GetEnumerator();
    }

    void Sort()
    {
        if (!_isDirty)
            return;

        _list.Sort((x, y) => x.GetType().GetPriority().CompareTo(y.GetType().GetPriority()));
        _isDirty = false;
    }
}