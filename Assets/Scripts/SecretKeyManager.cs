using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretKeyManager : MonoBehaviour
{
    public GameObject laserWall;
    public AudioSource sound;

    private void OnTriggerEnter2D(Collider2D collision)
    {   
        if (collision.gameObject.tag == KnownGameObjects.Player) 
        {
            sound.Play();
            
            laserWall.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
