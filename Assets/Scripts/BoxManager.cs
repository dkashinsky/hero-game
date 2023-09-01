using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxManager : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == KnownGameObjects.Player)
        {
            GetComponent<AudioSource>().Play();
        }
    }
}
