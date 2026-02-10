public class MockDataSerializer : IDataSerializer
/*
We want fully deterministic unit testing:
That is why we implement another mock to assign and track calls and their results.
Note the usage of public fields for the fake return valuees.
*/
{
    public string fakeSerializeResult;
    public Data fakeDeserializeResult;

    public bool DidCallSerialize { get; private set; }
    public Data SerializeDataParam { get; private set; }

    public bool DidCallDeserialize { get; private set; }
    public string DeserializeJsonParam { get; private set; }

    public string Serialize(Data data)
    {
        DidCallSerialize = true;
        SerializeDataParam = data;
        return fakeSerializeResult;
    }

    public Data Deserialize(string json)
    {
        DidCallDeserialize = true;
        DeserializeJsonParam = json;
        return fakeDeserializeResult;
    }
}