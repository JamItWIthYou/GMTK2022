using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public LevelGenerator levelGenerator;
    public int x;
    public int y;

    void Update() {
        if (y-1 < levelGenerator.tileCollidables.GetLength(1)) {
            if (levelGenerator.tileCollidables[x, y-1]==CollisionType.None) { y--; };
        }
        if(Input.GetKeyDown("a")) {
            if (levelGenerator.tileCollidables[x-1, y]==CollisionType.None) {x--;}
            else if (levelGenerator.tileCollidables[x-1, y]==CollisionType.Full) {
                if(levelGenerator.tileCollidables[x-1, y+1]==CollisionType.None) {x--;y++;}
            }
        }
        if(Input.GetKeyDown("d")) {
            if (levelGenerator.tileCollidables[x+1, y]==CollisionType.None) {x++;}
            else if (levelGenerator.tileCollidables[x+1, y]==CollisionType.Full) {
                if(levelGenerator.tileCollidables[x+1, y+1]==CollisionType.None) {x++;y++;}
            }
        }
        transform.position = new Vector3(x, y, 0);
    }
}
