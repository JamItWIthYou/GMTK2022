using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public LevelGenerator levelGenerator;

    public bool isOnHalfBlock;
    public int x;
    public int y;

    void Start() {
        
    }

    void Update() {
        if (y-1 < levelGenerator.tileCollidables.GetLength(1)) {
            if (levelGenerator.tileCollidables[x, y-1]==CollisionType.None) { y--; };
        }
        if(Input.GetKeyDown("a")) {
            if (levelGenerator.tileCollidables[x-1, y]==CollisionType.None) {
                if (levelGenerator.tileCollidables[x-1, y-1]==CollisionType.Half) {
                    isOnHalfBlock = true;
                } else {
                    isOnHalfBlock = false;
                }
                x--;
            } else if (levelGenerator.tileCollidables[x-1, y]==CollisionType.Half) {
                isOnHalfBlock = true;
                x--;
                y++;
            }
        }
        transform.position = new Vector3(x, y + (isOnHalfBlock?(-0.5f):0), 0);
    }
}
