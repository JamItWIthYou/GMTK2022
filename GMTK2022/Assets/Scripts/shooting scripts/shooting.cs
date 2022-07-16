using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooting : MonoBehaviour
{   private Camera mainCam;
    private Vector3 mousePos;
    public GameObject bullet;
    public Transform bulletTransform;
    public bool canFire;
    private float timer;
    public float timeBetweenFiring;
    
    // Start is called before the first frame update
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        

        
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        Vector3 rotation = mousePos - transform.position;

        float rotz = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, rotz);

       
        
        if(Input.GetMouseButtonDown(0) && canFire)
        {
            
        Instantiate(bullet, bulletTransform.position, Quaternion.identity);
        canFire = false;
        }

    }
}
