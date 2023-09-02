using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    public GameObject hero;
    public GameObject gunJoint;
    public GameObject gun;
    public GameObject bulletPoint;
    public GameObject bullet;
    private float bulletSpeed = 8f;
    private bool isInScreen;
    private float fireRate = 3f; // fire once in 3 seconds
    private float fireTimer = 0;
    private AnimationEventHandler animationHandler;

    void Awake()
    {
        animationHandler = gun.GetComponent<AnimationEventHandler>();
    }

    private void OnEnable()
    {
        animationHandler.OnFinish += OnFireAnimationFinish;
    }

    private void OnDisable()
    {
        animationHandler.OnFinish -= OnFireAnimationFinish;
    }

    void Update()
    {
        AimAtHero();
        FireAtHero();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == KnownGameObjects.ScreenSensor)
        {
            isInScreen = true;
            // to activate gun immediately after entering the screen
            fireTimer = fireRate;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == KnownGameObjects.ScreenSensor)
        {
            isInScreen = false;
        }
    }

    private void AimAtHero()
    {
        var deltaX = gunJoint.transform.position.x - hero.transform.position.x;
        var deltaY = gunJoint.transform.position.y - hero.transform.position.y;
        var angle = Math.Atan2(deltaY, deltaX) * (180 / Math.PI);

        gunJoint.transform.eulerAngles = new Vector3(0f, 0f, (float)angle);
    }

    private void FireAtHero()
    {
        // fire when bullet is not in use, the fun is in screen, and time interval has passed
        if (!bullet.activeSelf && isInScreen && fireTimer > fireRate)
        {
            fireTimer = 0;

            //play fire animation
            gun
                .GetComponent<Animator>()
                .SetBool("Charge", true);
        }

        fireTimer += Time.deltaTime;
    }

    private void OnFireAnimationFinish()
    {
        // turn off animation
        gun
            .GetComponent<Animator>()
            .SetBool("Charge", false);

        if (isInScreen) 
        {
            // enable bullet at starting location
            bullet.transform.position = bulletPoint.transform.position;
            bullet.SetActive(true);

            // fire bullet by applying force to it in direction to the player
            var vector = hero.transform.position - gunJoint.transform.position;
            vector.Normalize();
            bullet
                .GetComponent<Rigidbody2D>()
                .AddForce(vector * bulletSpeed, ForceMode2D.Impulse);

            // play sound once bullet is fired
            GetComponent<AudioSource>().Play();
        }
    }
}
