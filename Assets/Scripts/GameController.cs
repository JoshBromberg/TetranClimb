using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private int score;
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
        score += i;
        Instantiate(g, t.position, t.rotation);
    }
}
