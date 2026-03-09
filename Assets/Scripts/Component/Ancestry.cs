using UnityEngine;
using System.Collections.Generic;

public interface IAncestry
{
    string Title { get; }
    string Description { get; }
    Rarity Rarity { get; }
    List<IAttributeProvider> AttributeProviders { get; }
}

public class Ancestry : MonoBehaviour, IAncestry
{
    public string Title
    {
        get { return _title; }
    }
    [SerializeField] string _title;

    public string Description
    {
        get { return _description; }
    }
    [SerializeField] string _description;

    public Rarity Rarity
    {
        get { return _rarity; }
    }
    [SerializeField] Rarity _rarity;

    public List<IAttributeProvider> AttributeProviders
    {
        get
        {
            return new List<IAttributeProvider>(gameObject.GetComponents<IAttributeProvider>());
        }
    }

    public void Setup(string title, string description, Rarity rarity)
    {
        _title = title;
        _description = description;
        _rarity = rarity;
    }
}