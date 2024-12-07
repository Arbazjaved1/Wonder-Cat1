using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawMoving : MonoBehaviour
{
    public float Movementdistance;
    public float speed;

    private bool movingleft;
    private float leftedge;
    private float rightedge;

    // Start is called before the first frame update
    private void Awake()
    {
        leftedge = transform.position.x - Movementdistance;
        rightedge = transform.position.x + Movementdistance;
    }

    private void Update()
    {
        if (movingleft)
        {
            if (transform.position.x > leftedge)
            {
                transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
            {
                movingleft = false;
            }
        }
        else
        {
            if (transform.position.x < rightedge)
            {
                transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
            {
                movingleft = true;
            }
        }
    }
}