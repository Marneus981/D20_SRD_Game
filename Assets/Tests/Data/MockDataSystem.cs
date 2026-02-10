public class MockDataSystem : IDataSystem
/*
Made for testing Entities:
   All we care about is that we can Create a new instance of our game Data.
   Done so we save on injections during actual unit testing. 
*/
{
    public Data Data { get; private set; }

    public void Create()
    {
        Data = new Data();
    }

    public void Delete()
    {
        Data = null;
    }

    public bool HasFile()
    {
        return false;
    }

    public void Load()
    {
        
    }

    public void Save()
    {
        
    }
}