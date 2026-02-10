using NUnit.Framework;

public class DataSystemTests
{
    //Create mock fields as well as the system we will be testing "the sut"
    MockDataSerializer mockDataSerializer = new MockDataSerializer();
    MockDataStore mockDataStore = new MockDataStore();
    DataSystem sut = new DataSystem();

    [SetUp]
    public void SetUp()
    //Register mocks with their corresponding interfaces
    {
        IDataSerializer.Register(mockDataSerializer);
        IDataStore.Register(mockDataStore);
    }

    [TearDown]
    //We clean mock registrations
    public void TearDown()
    {
        IDataSerializer.Reset();
        IDataStore.Reset();
    }

    //We create a test for each mehod in the system:
    [Test]    
    public void Create_InitsData()
    /*
    Create invocation test: expect system's data model to be null before invocation;
    Non-null after.
    */
    {
        var dataBefore = sut.Data;
        sut.Create();
        Assert.IsNull(dataBefore);
        Assert.IsNotNull(sut.Data);
    }

    [Test]
    public void Delete_WrapsStore()
    /*
    Delete invocation test: expect DidCallDelete to be true after calling on the system
    */
    {
        sut.Delete();
        Assert.IsTrue(mockDataStore.DidCallDelete);
    }

    [Test]
    public void HasFile_WrapsStore()
    /*
    HasFile invocation test: expect DidCallHasFile to be true after calling on the system;
    Also expect the system's return value to match the fakeHasFile value we set on the mock.
    */
    {
        mockDataStore.fakeHasFile = true;
        var result = sut.HasFile();
        Assert.IsTrue(mockDataStore.DidCallHasFile);
        Assert.AreEqual(mockDataStore.fakeHasFile, result);
    }

    [Test]
    public void Save_Success()
    /*
    Save invocation test: expect DidCallSerialize, DidCallWrite to be true after calling on system;
    Also expect the parameter passed to Serialize to be the system's data and
    the parameter passed to Write to be the fakeSerializeResult we set on the mock.
    */
    {
        mockDataSerializer.fakeSerializeResult = "abc123";
        sut.Create();
        sut.Data.version = 1;
        sut.Save();
        Assert.IsTrue(mockDataSerializer.DidCallSerialize);
        Assert.AreEqual(sut.Data, mockDataSerializer.SerializeDataParam);
        Assert.IsTrue(mockDataStore.DidCallWrite);
        Assert.AreEqual(mockDataSerializer.fakeSerializeResult, mockDataStore.WriteJsonParam);
    }

    [Test]
    public void Load_Success()
    /*
    Load invocation test: expect DidCallRead, DidCallDeserialize to be true after calling on system;
    Also expect DeserializeJsonParam to be equal to fakeReadResult (from mock read),
    also expeeect system's data to be equal to fakeDeserializeResult (from Load setting Data to
    the mock deserialization result).

    */
    {
        mockDataSerializer.fakeDeserializeResult = new Data();
        mockDataStore.fakeReadResult = "abc123";
        sut.Load();
        Assert.IsTrue(mockDataStore.DidCallRead);
        Assert.IsTrue(mockDataSerializer.DidCallDeserialize);
        Assert.AreEqual(mockDataStore.fakeReadResult, mockDataSerializer.DeserializeJsonParam);
        Assert.AreEqual(mockDataSerializer.fakeDeserializeResult, sut.Data);
    }
}