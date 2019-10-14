﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KonamiCode : MonoBehaviour
{
    //Public Variables

    //Private Variables
    private KeyCode[] konamiCode =
    {
        KeyCode.UpArrow,
        KeyCode.UpArrow,
        KeyCode.DownArrow,
        KeyCode.DownArrow,
        KeyCode.LeftArrow,
        KeyCode.RightArrow,
        KeyCode.LeftArrow,
        KeyCode.RightArrow,
        KeyCode.Z,
        KeyCode.X
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