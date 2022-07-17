using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileControl : MonoBehaviour
{
    public LevelGenerator levelGenerator;
    public TurnController turnController;
    [Range(0.0f, 5.0f)]
    public float eRad;
    [Range(0, 10)]
    public int damage;
    void Start() {
        levelGenerator = GameObject.FindWithTag("LevelGenerator").GetComponent<LevelGenerator>();
        turnController = GameObject.FindWithTag("TurnController").GetComponent<TurnController>();
    }
    void FixedUpdate() {
        CheckForCollision();
    }
    void CheckForCollision() {
        int x = (int)Mathf.Round(transform.position.x);
        int y = (int)Mathf.Round(transform.position.y);
        if( (x >= 0) && (x<levelGenerator.tileCollidables.GetLength(0)) && (y >= 0) && (y<levelGenerator.tileCollidables.GetLength(1))) {
            if (levelGenerator.tileCollidables[x,y]==CollisionType.Full) Explode(new IntVector2(x, y));
        }
    }
    void Explode(IntVector2 ePos) {
        foreach(Character character in turnController.characterList) {
            if(Mathf.Abs(Vector2.Distance(transform.position, character.transform.position)) < eRad) {
                character.TakeDamage(damage);
            }
        }
        foreach(IntVector2 pos in GetDiamondToRemove((int)Mathf.Round(eRad), ePos)) {
            Debug.Log("X: "+pos.x+" Y: "+pos.y);
            levelGenerator.tileCollidables[pos.x,pos.y] = CollisionType.None;
            Destroy(levelGenerator.tileMap[pos.x,pos.y]);
        }
        Destroy(gameObject);
    }
    List<IntVector2> GetDiamondToRemove(int eRad, IntVector2 ePos) {
        List<IntVector2> listOfPositions = new List<IntVector2>();
        List<IntVector2> IncreaseDiamond(int eRad, IntVector2 ePos, int y, List<IntVector2> prevPositions) {
            for(int x=-(eRad-y);x<=(eRad-y);x++) {
                prevPositions.Add(new IntVector2(ePos.x+x,ePos.y+y));
            }
            if (y==0) {
                return ReduceDiamond(eRad, ePos, y-1, prevPositions);
            } else {
                return IncreaseDiamond(eRad, ePos, y-1, prevPositions);
            }
        }
        List<IntVector2> ReduceDiamond(int eRad, IntVector2 ePos, int y, List<IntVector2> prevPositions) {
            for(int x=-(eRad+y);x<=(eRad+y);x++) {
                prevPositions.Add(new IntVector2(ePos.x+x,ePos.y+y));
            }
            if (y==-eRad) {
                return prevPositions;
            } else {
                return ReduceDiamond(eRad, ePos, y-1, prevPositions);
            }
        }
        IncreaseDiamond(eRad, ePos, eRad, listOfPositions);
        return listOfPositions;
    }
    
}
