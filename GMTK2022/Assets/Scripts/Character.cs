using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Character : MonoBehaviour, IComparable<Character>
{
    public int turnPriority;
    public TurnController turnController;
    public abstract void BeginTurn();
    public int CompareTo(Character other){
        if(other == null) {return 1;}
        //Return the difference in priority
        return other.turnPriority-turnPriority;
    }
}
