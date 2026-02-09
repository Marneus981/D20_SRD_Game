using UnityEngine;
/*
Each system will be responsible for its own basic CRUD operations (Create, Read, Update and Delete)
but we will handle serialization and file storage separately.
The goal for this system is to create the foundation for easy data persistence (save and load game data – the entire object graph).
This will also allow us to save the output as JSON.
*/
[System.Serializable]
public partial class Data
{
    public int version;
}
