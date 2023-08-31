using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserManager : MonoBehaviour
{
    private GameObject hero = null;
    private int damagePerSecond;
    private float damageFrequency;
    private bool isHeroCollided;
    private float laserTimer;

    // Start is called before the first frame update
    void Start()
    {
        damagePerSecond = 20;
        damageFrequency = 0.5f; // every half second
        laserTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (hero != null)
        {
            laserTimer += Time.deltaTime;

            if (laserTimer >= damageFrequency)
            {
                ApplyDamageToHero();
                laserTimer = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {   
        if (collision.gameObject.tag == KnownGameObjects.Player) 
        {
            hero = collision.gameObject;

            ApplyDamageToHero();
            laserTimer = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == KnownGameObjects.Player)
        {
            hero = null;
        }
    }

    private void ApplyDamageToHero()
    {
        hero?
            .GetComponent<HeroManager>()
            .GetHeroState()
            .UpdateHealth(-(int)Math.Floor(damagePerSecond * damageFrequency));
    }
}
