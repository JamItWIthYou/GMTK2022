using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public LevelGenerator levelGenerator;
    public int x;
    public int y;

    public int movesRemaining;

    void Update() {
        if (movesRemaining>0) {
            Controls();
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
}
