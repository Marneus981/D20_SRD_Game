public class MockDataStore : IDataStore
/*
For testing, we want control over the returned result of HasFile and Read.
We also want to know if methods got called accordingly (note the flags).
When calling Write, we track the trigger and the passed parameter.
*/
{
    public bool fakeHasFile;
    public string fakeReadResult;

    public bool DidCallDelete { get; private set; }
    public bool DidCallHasFile { get; private set; }
    public bool DidCallRead { get; private set; }
    public bool DidCallWrite { get; private set; }
    public string WriteJsonParam { get; private set; }

    public void Delete()
    {
        DidCallDelete = true;
    }

    public bool HasFile()
    {
        DidCallHasFile = true;
        return fakeHasFile;
    }

    public string Read()
    {
        DidCallRead = true;
        return fakeReadResult;
    }

    public void Write(string json)
    {
        DidCallWrite = true;
        WriteJsonParam = json;
    }
}