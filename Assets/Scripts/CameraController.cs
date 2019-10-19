using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private AudioClip loop;
    private Transform player;
    private AudioSource s;
    private bool looped = false;
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
        if (s.isPlaying == false && looped == false)
        {
            s.clip = loop;
            s.Play();
            s.loop = true;
            looped = true;
        }
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
    }
}
