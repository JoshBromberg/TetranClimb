﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private int score;
    private int[] fansActive = { 4, 4, 4, 4 };
    [SerializeField]
    private GameObject powerCapsule;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddScore(int i)
    {
        score += i;
    }
    public void AddScore(int i, GameObject g, Transform t)
    {
        AddScore(i);
        Instantiate(g, t.position, t.rotation);
        if (i == 100 || i == 50)
        {
            Instantiate(powerCapsule, t.position, t.rotation);
        }
        if (g.name.Contains("Fan"))
        {
            int fanArray = t.position.y > transform.position.y ? 0 : 2;
            fanArray = t.position.x > transform.position.x ? fanArray : fanArray + 1;
            --fansActive[fanArray];
            if (fansActive[fanArray] == 0)
            {
                Instantiate(powerCapsule, t.position, t.rotation);
            }
        }
    }
}