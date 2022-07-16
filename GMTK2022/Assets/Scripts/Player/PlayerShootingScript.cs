using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootingScript : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject projectileToSpawn;
    public Transform bulletTransform;
    public bool canFire;
    public GameObject createdProjectile;
    public float fireForce; 


    void Update()
    {
        Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 mouseRelDistance = mousePos - transform.position;

        // Calculate the direction of the mouse from this object
        float zrotation = Mathf.Atan2(mouseRelDistance.y, mouseRelDistance.x) * Mathf.Rad2Deg;

        // Set rotation in order to change where the spawn indicator is
        transform.rotation = Quaternion.Euler(0, 0, zrotation);

        if(Input.GetMouseButtonDown(0) && canFire)
        {
            createdProjectile = Instantiate(projectileToSpawn, bulletTransform.position, Quaternion.identity);
            Rigidbody2D projRB = createdProjectile.GetComponent<Rigidbody2D>();
            projRB.velocity = new Vector2(mouseRelDistance.x, mouseRelDistance.y).normalized * fireForce;
            canFire = false;
        }

    }
}
