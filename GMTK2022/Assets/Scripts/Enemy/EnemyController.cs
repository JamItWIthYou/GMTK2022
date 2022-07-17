using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Character
{
    // Might need refactor for GameController
    public Transform player;
    public LevelGenerator levelGenerator;


    // Shoot-related
    public GameObject projectile;
    public GameObject createdProj;
    public float shootSpeed;
    [Range(0.0f, 1.0f)]
    public float minShootRand;
    [Range(1.0f, 2.0f)]
    public float maxShootRand;

    // Movement-related
    public int x;
    public int y;
    [Range(0.0f, 1.0f)]
    public float moveRandomness;
    private int movesLeft;
    public int movesEachTurn;
    public float moveInterval;
    private float timeSinceMove;

    void Start() {
        turnController.AddToTurnControl(this);
    }
    
    void Update () {
        if (this == turnController.currentCharacter){
            if (isDead()){
                turnController.RemoveFromTurnControl(this);
                Destroy(gameObject);
            }else{
                timeSinceMove += Time.deltaTime;
                if (timeSinceMove>moveInterval) {
                    timeSinceMove = 0;
                    if(movesLeft>0){
                        Move();
                        movesLeft--;
                    } else if (movesLeft==0) {
                        Shoot();
                        movesLeft--;
                    } else {
                        turnController.EndTurn(this);
                    }
                }
            }
        }
        Gravity();
        transform.position = new Vector2(x, y);
    }

    void Move () {
        if(Random.value > moveRandomness){
            if (player.position.x > transform.position.x) MoveRight(); else MoveLeft();
        } else {
            if(Random.value < 0.5f) MoveRight(); else MoveLeft();
        }
    }
    void MoveLeft() {
        if (levelGenerator.tileCollidables[x-1, y]==CollisionType.None) {x--;}
            else if (levelGenerator.tileCollidables[x-1, y]==CollisionType.Full) {
                if(levelGenerator.tileCollidables[x-1, y+1]==CollisionType.None) {x--;y++;}
            }
    }
    void MoveRight() {
        if (levelGenerator.tileCollidables[x+1, y]==CollisionType.None) {x++;}
            else if (levelGenerator.tileCollidables[x+1, y]==CollisionType.Full) {
                if(levelGenerator.tileCollidables[x+1, y+1]==CollisionType.None) {x++;y++;}
            }
    }
    void Gravity() {
        if (y-1 < levelGenerator.tileCollidables.GetLength(1)) {
            if (levelGenerator.tileCollidables[x, y-1]==CollisionType.None) {y--;};
        }
    }
    void Shoot() {
        createdProj = Instantiate(projectile, transform.position, Quaternion.identity, levelGenerator.transform);
        createdProj.GetComponent<Rigidbody2D>().velocity = new Vector2(shootSpeed*Random.Range(minShootRand, maxShootRand), CalculateAddForceY(player));
    }
    float CalculateAddForceY(Transform player) {
        Vector2 relDist = CalculatePlayerRelativeDistance(player);
        // y = axx+bx+c
        float a = Physics.gravity.y/2;
        float x1 = transform.position.x;
        float x2 = player.position.x;
        float y1 = transform.position.y;
        float y2 = player.position.y;
        // I am so sorry.
        float b = (-(((a*(x2*x2))-(a*(x1*x1)))-(y2-y1)))/(x2-x1);
        float c = a*x1*x1+b*x1-y1;
        // Dy/Dx
        float Dy = (2*a*x1+b)/shootSpeed;
        return Dy;
    }
    Vector2 CalculatePlayerRelativeDistance(Transform player) {
        return player.position - transform.position;
    }
    public override void BeginTurn() {
        movesLeft = movesEachTurn;
        Debug.Log("Enemy turn");
    }
}
