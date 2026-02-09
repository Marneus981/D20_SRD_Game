using UnityEngine;
using System;
 
[Serializable]  ///To easily save/display in inspector
public partial struct DiceRoll
{
    public int count; //No of dice
    public int sides; //No of sides
    public int bonus; //Modifier added to the roll

    ///Constructors
    public DiceRoll(int sides) //e.g. d6
    {
        this.count = 1;
        this.sides = sides;
        this.bonus = 0;
    }
    public DiceRoll(int count, int sides) // e.g. 3d6
    {
        this.count = count;
        this.sides = sides;
        this.bonus = 0;
    }
    public DiceRoll(int count, int sides, int bonus) //e.g. 4d8 + 3
    {
        this.count = count;
        this.sides = sides;
        this.bonus = bonus;
    }

    //Common die types: d4,d6,d8,d10,d12,d20,d100 (QUESTION: do I needa define them inside the struct dfn?)

    public static readonly DiceRoll D6 = new DiceRoll(6);
    public static readonly DiceRoll D8 = new DiceRoll(8);
    public static readonly DiceRoll D10 = new DiceRoll(10);
    public static readonly DiceRoll D12 = new DiceRoll(12);
    public static readonly DiceRoll D20 = new DiceRoll(20);
    public static readonly DiceRoll D100 = new DiceRoll(100);
}




