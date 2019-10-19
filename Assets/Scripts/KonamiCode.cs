using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KonamiCode : MonoBehaviour
{
    //Public Variables

    //Private Variables
    private KeyCode[] konamiCode =
    {
        KeyCode.W,
        KeyCode.W,
        KeyCode.S,
        KeyCode.S,
        KeyCode.A,
        KeyCode.D,
        KeyCode.A,
        KeyCode.D,
        KeyCode.Mouse0,
        KeyCode.Mouse1
    };
    private int konami = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (konami < konamiCode.Length)
        {
            if (Input.GetKeyDown(konamiCode[konami]))
            {
                ++konami;
                if (konami == konamiCode.Length)
                {
                    try
                    {
                        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().KonamiCode();
                    }
                    catch (System.NullReferenceException) { }
                }
            }
            else
            {
                foreach (var i in konamiCode)
                {
                    if (Input.GetKeyDown(i))
                    {
                        konami = 0;
                    }
                }
            }
        }
    }
}
