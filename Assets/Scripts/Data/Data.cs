using UnityEngine;
/*
Each system will be responsible for its own basic CRUD operations (Create, Read, Update and Delete)
but we will handle serialization and file storage separately.
The goal for this system is to create the foundation for easy data persistence (save and load game data – the entire object graph).
This will also allow us to save the output as JSON.
Since our class is partial, we may optionally define any future data we will need in the same file as the system that will use it.
*/
[System.Serializable]
public partial class Data
/*
Class is partial: 
We may optionally define any future data we will need in the same file as the system that will use it. 

The model was also marked as Serializable:
Data persists as json, via Unity’s JsonUtility. 
Any additional fields we add, even in other partial definitions, will automatically be included when we save this model.
*/
{
    public int version;
}
