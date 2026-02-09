using UnityEngine;

public class DiceRollSystem : IDiceRollSystem
{
    public int Roll(DiceRoll diceRoll) //Roll die
    {
        int result = diceRoll.bonus; //Start with mod
        for (int i = 0; i < diceRoll.count; i++) //Iterate through the dice
            result += IRandomNumberGenerator.Resolve().Range(1, diceRoll.sides + 1); //Mind the number of sides (non-inclusive)
        return result;
    }
}

public partial struct DiceRoll
/*Partial struct for rolling a die

Here, the data is separate from the logic (the logic is really just a wrapper for the system)
This defines “part” of the implementation of a DiceRoll. 
It defines a method named Roll that wraps whatever system has been injected into the interface.
Note that when using a partial like this, that all of the definitions must include the partial keyword. 
*/
{
    public int Roll()
    {
        return IDiceRollSystem.Resolve().Roll(this);
    }
}

public interface IDiceRollSystem : IDependency<IDiceRollSystem>
/*Public interface for rolling a die, with the actual logic in DiceRollSystem

This interface inherits the generic IDependency interface so that we can utilize the interface injection pattern.
*/
{
    int Roll(DiceRoll diceRoll);
}