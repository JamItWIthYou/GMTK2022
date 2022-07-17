using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Character : MonoBehaviour, IComparable<Character>
{
    [Range(0, 100)]
    public int hitPoints;
    public int turnPriority;
    public TurnController turnController;
    public abstract void BeginTurn();
    public void TakeDamage(int damage) {
        hitPoints -= damage;
    }
    public bool isDead(){
        if (hitPoints<1){
            return true;
        }else{
            return false;
        }

    }
    public int CompareTo(Character other){
        if(other == null) {return 1;}
        //Return the difference in priority
        return other.turnPriority-turnPriority;
    }
}
