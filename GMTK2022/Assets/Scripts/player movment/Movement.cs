using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D rb2D;

    private float moveSpeed;
    private float moveHorizontal;



    // Start is called before the first frame update
    void Start()
    {
        
        rb2D = gameObject.GetComponent<Rigidbody2D>();

        moveSpeed = 1f;


    }

    // Update is called once per frame
    void Update()
    {
        
        moveHorizontal = Input.GetAxisRaw("Horizontal");


    }

    void FixedUpdate()
    {
        if(moveHorizontal > 0f || moveHorizontal < 0f)
        {
            rb2D.AddForce(new Vector2(moveHorizontal * moveSpeed, 0f), ForceMode2D.Impulse);
        }
        
    }
}
