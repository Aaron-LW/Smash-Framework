namespace Smash.EntityComponentSystem;

public interface ILogicSystem
{ 
    public void Update(double deltaTime, Entity[] entities);
}