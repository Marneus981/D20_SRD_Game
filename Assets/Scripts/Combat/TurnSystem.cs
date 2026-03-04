using System.Collections.Generic;
public interface ITurnSystem : IDependency<ITurnSystem>
{
    Entity Current { get; }
    int ActionsRemaining { get; }
    int AttackCount { get; }
    bool IsComplete { get; }

    void Begin(Entity entity);
    void TakeAction(int actionCost, bool isAttack);
    List<Entity> InReach { get; set; }
}

public class TurnSystem : ITurnSystem
{
    public Entity Current { get { return current; } }
    Entity current;

    public int ActionsRemaining { get { return actionsRemaining; } }
    int actionsRemaining;

    public int AttackCount { get { return attackCount; } }
    int attackCount;

    public bool IsComplete { get { return actionsRemaining == 0; } }

    public void Begin(Entity entity)
    {
        current = entity;
        actionsRemaining = 3;
        attackCount = 0;
    }

    public void TakeAction(int actionCost, bool isAttack)
    {
        actionsRemaining -= actionCost;
        if (isAttack) attackCount++;
    }
    public List<Entity> InReach { get; set; }
}