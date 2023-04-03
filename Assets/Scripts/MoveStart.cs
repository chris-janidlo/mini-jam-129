using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveStart : MonoBehaviour
{

    public int movespeed;
    private Vector3 start;

    // Start is called before the first frame update
    void Start()
    {
        start = new Vector3(1000, 70, 0);
        transform.position = start;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + (Vector3.left) * movespeed * Time.deltaTime;

        if(transform.position.x < -600)
        {
            transform.position = start;
        }
    }
}
