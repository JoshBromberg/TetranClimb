using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private AudioClip loop;
    private int loopTimer = 531;
    private Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
        if (loopTimer > 0)
        {
            --loopTimer;
            if (loopTimer <= 0)
            {
                GetComponent<AudioSource>().clip = loop;
                GetComponent<AudioSource>().Play();
            }
        }
    }
}
