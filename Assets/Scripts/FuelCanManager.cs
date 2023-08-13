using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelCanManager : MonoBehaviour
{
    public GameObject hero;
    private int healthPoints = 10;

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
        if (collision.gameObject == hero)
        {
            var heroState = hero
                .GetComponent<HeroManager>()
                .GetHeroState();

            if (!heroState.IsHealthy)
            {
                heroState.UpdateHealth(healthPoints);
                gameObject.SetActive(false);
            }
        }
    }
}
