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
    [Range(0.0f, 5.0f)]
    public float eRad;
    [Range(0, 10)]
    public int damage;

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
        levelGenerator = GameObject.FindWithTag("LevelGenerator").GetComponent<LevelGenerator>();
        turnController = GameObject.FindWithTag("TurnController").GetComponent<TurnController>();
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
    void FixedUpdate(){
        CheckForCollision();
    }
    void CheckForCollision() {
        int x = (int)Mathf.Round(projectile.transform.position.x);
        int y = (int)Mathf.Round(projectile.transform.position.y);
        Debug.Log(x+" "+y);
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
