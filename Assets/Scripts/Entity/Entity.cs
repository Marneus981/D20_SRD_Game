[System.Serializable]
public partial struct Entity
/*
Note 1: Structs are value types so they are passed by value, 
rather than by reference. Contrary to more complex types, 
structs do not generate "garbage that needs to be collected later".
Note 2: partial keyword means we can save, load an entity, as well as add
functionality in separate files (e.g. wrappers for system functionality, etc.)
Note 3: Serializable; saved and loaded as part of game data.
*/
{
    public readonly int id;

    public Entity(int id)
    {
        this.id = id;
    }
    public static readonly Entity None = new Entity(0); //Null ref implemented for usefulness
    public static bool operator ==(Entity lhs, Entity rhs) => lhs.id == rhs.id;//= op bc no address comparison
    public static bool operator !=(Entity lhs, Entity rhs) => !(lhs == rhs);
    public override bool Equals(object obj) => this.Equals((Entity)obj);
    public bool Equals(Entity p) => this == p;
    public override int GetHashCode() => id.GetHashCode();
}