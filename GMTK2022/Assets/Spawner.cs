using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject Dice;
    private GameObject dice;
    public Vector3 dicePosition = new Vector3(1,0,0);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("q"))
        {
            dice = Instantiate(Dice, transform.position + dicePosition, transform.rotation);
            dice.GetComponent<Rigidbody2D>().AddForce(new Vector2(10f,20f), ForceMode2D.Impulse);
        }
    }
}
