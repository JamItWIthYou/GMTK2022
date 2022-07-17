using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Character
{
    public LevelGenerator levelGenerator;
    public PlayerShootingScript playerShootingScript;    
    public int x;
    public int y;
    public int movesEachTurn;
    private int movesRemaining;

    void Start () {
        turnController.AddToTurnControl(this);
        Debug.Log(this);
    }
    void Update () {
        if(this == turnController.currentCharacter){
            if(isDead()){
                turnController.RemoveFromTurnControl(this);
                Destroy(gameObject);
            }
            if (movesRemaining>0) Controls();
            else if (!playerShootingScript.canFire) turnController.EndTurn(this);
        }
        Gravity();
        transform.position = new Vector3(x, y, 0);
    }
    void Controls () {
        if(Input.GetKeyDown("a")) {
            if (levelGenerator.tileCollidables[x-1, y]==CollisionType.None) {x--;movesRemaining--;}
            else if (levelGenerator.tileCollidables[x-1, y]==CollisionType.Full) {
                if(levelGenerator.tileCollidables[x-1, y+1]==CollisionType.None) {x--;y++;movesRemaining--;}
            }
        }
        else if(Input.GetKeyDown("d")) {
            if (levelGenerator.tileCollidables[x+1, y]==CollisionType.None) {x++;movesRemaining--;}
            else if (levelGenerator.tileCollidables[x+1, y]==CollisionType.Full) {
                if(levelGenerator.tileCollidables[x+1, y+1]==CollisionType.None) {x++;y++;movesRemaining--;}
            }
        }
    }
    void Gravity() {
        if (y-1 < levelGenerator.tileCollidables.GetLength(1)) {
            if (levelGenerator.tileCollidables[x, y-1]==CollisionType.None) {y--;};
        }
    }
    public override void BeginTurn() {
        Debug.Log("Player turn");
        movesRemaining=movesEachTurn;
        playerShootingScript.canFire = true;
    }
}
