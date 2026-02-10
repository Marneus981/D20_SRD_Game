public partial class Data
{
    public CoreSet<Entity> entities = new CoreSet<Entity>();
}
public interface IEntitySystem : IDependency<IEntitySystem>
{
        Entity Create();
        void Destroy(Entity entity);
}
public class EntitySystem : IEntitySystem
{
        /*
        Private members
            Note: both properties are wrapping the resolved system reference, 
            so they will use whatever system has been injected.
        */
        Data Data { get { return IDataSystem.Resolve().Data; } }
        IRandomNumberGenerator RNG { get { return IRandomNumberGenerator.Resolve(); } }

        public Entity Create()
        {
            Entity result;
            do
            {
                result = new Entity(RNG.Range(int.MinValue, int.MaxValue));
            }
            while (result.id == 0 || Data.entities.Contains(result));
                //This type of loop always runs at least once
            Data.entities.Add(result);
            return result;
        }

        public void Destroy(Entity entity)
        {
            Data.entities.Remove(entity);
        }
}