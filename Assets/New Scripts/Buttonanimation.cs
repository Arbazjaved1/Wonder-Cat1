using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttonanimation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void jumpbutton()
    {
        GetComponent<Animation>().Play("Button animation");
    }
}
