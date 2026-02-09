using UnityEngine;
//Test for DiceRoll and DiceRollSystem
public class Checkpoint : MonoBehaviour
{
    [SerializeField] DiceRoll diceRoll = DiceRoll.D6;
    DiceRollSystem system = new DiceRollSystem();

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Return))
        {
            var result = system.Roll(diceRoll);
            Debug.Log(result);
        }    
    }
}