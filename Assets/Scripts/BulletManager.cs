using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public AudioSource sound;
    private int healthDamagePoints = 15;
    private List<string> collisionObjects = new List<string>()
    {
        KnownGameObjects.Player,
        KnownGameObjects.Level
    };

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // apply damage if bullet collides with player
        if (collision.gameObject.tag == KnownGameObjects.Player)
        {
            collision.gameObject
                .GetComponent<HeroManager>()
                .GetHeroState()
                .UpdateHealth(-healthDamagePoints);

            sound.Play();
        }

        // disappear the bullet when it collides with player or level (floor)
        if (collisionObjects.Contains(collision.gameObject.tag))
            gameObject.SetActive(false);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == KnownGameObjects.ScreenSensor)
        {
            // deactivate bullet when it leaves the sceen so that it can be reused
            gameObject.SetActive(false);
        }
    }
}
