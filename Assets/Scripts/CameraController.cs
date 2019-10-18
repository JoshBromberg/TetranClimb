using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private AudioClip loop;
    private int loopTimer = 431; //531, 331
    private Transform player;
    private AudioSource s;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        s = GetComponent<AudioSource>();
        s.loop = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (s.isPlaying == false)
        {
            s.clip = loop;
            s.Play();
            s.loop = true;
        }
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
    }
}
