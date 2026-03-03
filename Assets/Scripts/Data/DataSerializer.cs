using UnityEngine;
/*
Separating Unity dependency: Serialization
Ability to serialize to json
*/
public interface IDataSerializer : IDependency<IDataSerializer>
{
    string Serialize(Data data);
    Data Deserialize(string json);
}

public class DataSerializer : IDataSerializer
/*
Serializer handles turning our Data model into JSON and creating a Data model from JSON. 
Wraps functionality that Unity provides via JsonUtility;
Bonus: Trivial to swap the serializer out for another library should it be desired.
Caveat with Unity's tools: missing built-in support for serializing Dictionaries, HashSets, etc.
*/
{
    public string Serialize(Data data)
    {
/*      var result = JsonUtility.ToJson(data); */
        return JsonUtility.ToJson(data);
/*      Debug.Log(result);
        return result; */
    }

    public Data Deserialize(string json)
    {
        return JsonUtility.FromJson<Data>(json);
    }
}