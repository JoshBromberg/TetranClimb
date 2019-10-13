using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteEnd : MonoBehaviour
{
    //To use this function, create a box collider around the screen via a GameObject (call it 'Boundry'), then give it this code

    private void OnTriggerExit2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
    }
}
