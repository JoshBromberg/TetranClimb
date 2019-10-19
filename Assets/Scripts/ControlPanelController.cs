using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPanelController : MonoBehaviour
{
    [SerializeField]
    private int instance;
    [SerializeField]
    private Sprite basic, active;
    private PlayerController p;
    private SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        p = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (sr.sprite == basic && instance == p.PowerCapsules)
        {
            sr.sprite = active;
        }
        else if (sr.sprite == active && instance != p.PowerCapsules)
        {
            sr.sprite = basic;
        }
    }
}
