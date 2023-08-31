using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    private int healthDamagePoints = 35;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == KnownGameObjects.Player)
        {
            collision.gameObject
                .GetComponent<HeroManager>()
                .GetHeroState()
                .UpdateHealth(-healthDamagePoints);

            gameObject.SetActive(false);
        }
    }
}
