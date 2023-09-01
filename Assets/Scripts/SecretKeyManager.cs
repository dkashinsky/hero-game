using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretKeyManager : MonoBehaviour
{
    public GameObject laserWall;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {   
        if (collision.gameObject.tag == KnownGameObjects.Player) 
        {
            gameObject.SetActive(false);
            laserWall.SetActive(false);
        }
    }
}
