using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserManager : MonoBehaviour
{
    public GameObject hero;
    private int damagePerSecond;
    private float damageFrequency;
    private bool isHeroCollided;
    private float laserCounter;

    // Start is called before the first frame update
    void Start()
    {
        damagePerSecond = 20;
        damageFrequency = 0.5f; // every half second
        laserCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (isHeroCollided)
        {
            laserCounter += Time.deltaTime;

            if (laserCounter >= damageFrequency)
            {
                ApplyDamageToHero();
                laserCounter = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isHeroCollided = collision.gameObject == hero;
        
        if (isHeroCollided) 
        {
            ApplyDamageToHero();
            laserCounter = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == hero)
        {
            isHeroCollided = false;
        }
    }

    private void ApplyDamageToHero()
    {
        hero
            .GetComponent<HeroManager>()
            .GetHeroState()
            .UpdateHealth(-(int)Math.Floor(damagePerSecond * damageFrequency));
    }
}
