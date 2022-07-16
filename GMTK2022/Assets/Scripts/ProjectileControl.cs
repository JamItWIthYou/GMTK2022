using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileControl : MonoBehaviour
{
    public LevelGenerator levelGenerator;
    public TurnController turnController;
    [Range(0.0f, 5.0f)]
    public float explosionRadius;
    [Range(0, 5)]
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
            if (levelGenerator.tileCollidables[x,y]==CollisionType.Full) Explode();
        }
    }
    void Explode() {
        foreach(Character character in turnController.characterList) {
            if(Mathf.Abs(Vector2.Distance(transform.position, character.transform.position)) < explosionRadius) {
                character.TakeDamage(damage);
            }
        }
        Destroy(gameObject);
    }
}
