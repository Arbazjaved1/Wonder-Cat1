using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionPannel : MonoBehaviour
{
    public GameObject exit;
    public GameObject Optionapnnel;
    public GameObject Mainmenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void EXIT()
    {
        exit.SetActive(true);
        Optionapnnel.SetActive(false);
        Mainmenu.SetActive(true);

    }
}
