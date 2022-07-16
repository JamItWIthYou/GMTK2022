using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public Transform player;

    public GameObject projectile;
    public GameObject createdProj;

    public float shootSpeed;
    [Range(0.0f, 1.0f)]
    public float minRand;
    [Range(1.0f, 2.0f)]
    public float maxRand;

    void Start() {
        InvokeRepeating("Shoot", 2.0f, 0.01f);
    }
    void Shoot() {
        createdProj = Instantiate(projectile, transform.position, Quaternion.identity, transform);
        createdProj.GetComponent<Rigidbody2D>().velocity = new Vector2(shootSpeed*Random.Range(minRand, maxRand), CalculateAddForceY(player));
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
}
