using System.IO;
using UnityEngine;

public interface IDataStore : IDependency<IDataStore>
/*
Interface Goal:
Wrap access to the File class. Following good practices,
unit tests won't be accessing disk. We can also modify the storage system easily
(location, encryption, how).
Note: more complex feats such as remote access may require async callbacks, etc.
*/
{
    bool HasFile();
    string Read();
    void Write(string json);
    void Delete();
}

public class DataStore : IDataStore
{
    public string FilePath { get; private set; }

    public DataStore(string fileName)
    {
        this.FilePath = string.Format("{0}/{1}.txt", Application.persistentDataPath, fileName);
    }

    public bool HasFile()
    {
        return File.Exists(FilePath);
    }

    public string Read()
    {
        if (File.Exists(FilePath))
            return File.ReadAllText(FilePath);
        return "";
    }

    public void Write(string json)
    {
        File.WriteAllText(FilePath, json);
    }

    public void Delete()
    {
        File.Delete(FilePath);
    }
}