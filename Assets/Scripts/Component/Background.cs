using UnityEngine;
using System.Collections.Generic;

public interface IBackground
{
	string Title { get; }
	Rarity Rarity { get; }
	string Summary { get; }
	List<IAttributeProvider> AttributeProviders { get; }
}

public class Background : MonoBehaviour, IBackground
{
	public string Title
	{
		get { return _title; }
		set { _title = value; }
	}
	[SerializeField] string _title;

	public Rarity Rarity
	{
		get { return _rarity; }
		set { _rarity = value; }
	}
	[SerializeField] Rarity _rarity;

	public string Summary
	{
		get { return _summary; }
		set { _summary = value; }
	}
	[SerializeField] string _summary;

	public List<IAttributeProvider> AttributeProviders
	{
		get
		{
			return new List<IAttributeProvider>(gameObject.GetComponents<IAttributeProvider>());
		}
	}
}