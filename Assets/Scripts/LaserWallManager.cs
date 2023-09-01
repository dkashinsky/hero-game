using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserWallManager : MonoBehaviour
{
    private int healthDamagePoints = 30;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == KnownGameObjects.Player)
        {
            collision.gameObject
                .GetComponent<HeroManager>()
                .GetHeroState()
                .UpdateHealth(-healthDamagePoints);
        }
    }
}
