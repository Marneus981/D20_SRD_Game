using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICombatSelectionIndicator : IDependency<ICombatSelectionIndicator>
{
    void SetPosition(Point value);
    void SetVisible(bool isVisible);
    void SetSpace(int value);
    void Mark(Entity entity);
}

public class CombatSelectionIndicator : MonoBehaviour, ICombatSelectionIndicator
{
    public void SetPosition(Point value)
    {
        transform.position = value;
    }

    public void SetVisible(bool isVisible)
    {
        GetComponent<SpriteRenderer>().enabled = isVisible;
    }

    private void OnEnable()
    {
        ICombatSelectionIndicator.Register(this);
    }

    private void OnDisable()
    {
        ICombatSelectionIndicator.Reset();
    }
    public void SetSpace(int tiles)//to reflect size
    {
        transform.localScale = new Vector3(tiles, tiles, tiles);
    }

    public void Mark(Entity entity)
    {
        SetPosition(entity.Position);
        SetSpace(entity.Size.ToTiles());
    }
}
