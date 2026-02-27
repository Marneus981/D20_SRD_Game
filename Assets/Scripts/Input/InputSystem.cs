using UnityEngine;

public enum InputAxis
{
    Vertical,
    Horizontal
}

public enum InputAction
{
    Confirm,
    Cancel
}
public interface IInputSystem : IDependency<IInputSystem>
{
    int GetAxisUp(InputAxis axis);
    bool GetKeyUp(InputAction action);
}
public class InputSystem : IInputSystem
//Note: diff GetKeyUp
{
    public int GetAxisUp(InputAxis axis)
    {
        switch (axis)
        {
            case InputAxis.Vertical:
                if (Input.GetKeyUp(KeyCode.UpArrow))
                    return 1;
                if (Input.GetKeyUp(KeyCode.DownArrow))
                    return -1;
                break;
            case InputAxis.Horizontal:
                if (Input.GetKeyUp(KeyCode.RightArrow))
                    return 1;
                if (Input.GetKeyUp(KeyCode.LeftArrow))
                    return -1;
                break;
        }
        return 0;
    }

    public bool GetKeyUp(InputAction action)
    {
        switch (action)
        {
            case InputAction.Confirm:
                return Input.GetKeyUp(KeyCode.Return);
            case InputAction.Cancel:
                return Input.GetKeyUp(KeyCode.Escape);
        }
        return false;
    }
}