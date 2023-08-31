using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class HeroManager : MonoBehaviour, IHeroAnimationContext
{
    public Text scoreText;
    public Text healthText;
    public float jumpForce = 7.0f;      // Amount of force added when the player jumps.
    private float movementSpeed = 7.0f; // The speed of the horizontal movement
    private float startX = 0f;
    private int score = 0;
    private Animator anim;
    private Rigidbody2D body;
    private AnimatedHeroState heroState;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        SetHeroState(new StandingState(this));
        startX = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (heroState.IsAlive)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) && !heroState.IsJumping)
            {
                // Add a vertical force to the player.
                body.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                heroState.UpdateIsJumping(true);
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
                transform.position = new Vector3(
                    transform.position.x + Time.deltaTime * movementSpeed,
                    transform.position.y,
                    0f);
                heroState.UpdateIsMoving(true);
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
                transform.position = new Vector3(
                    transform.position.x - Time.deltaTime * movementSpeed,
                    transform.position.y,
                    0f);
                heroState.UpdateIsMoving(true);
            }

            if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
            {
                heroState.UpdateIsMoving(false);
            }

            CalculateScore();
        }
        
        UpdateHealthUI();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        heroState.UpdateIsJumping(false);
    }

    public AnimatedHeroState GetHeroState()
    {
        return heroState;
    }

    public void SetHeroState(AnimatedHeroState state)
    {
        heroState = state;
        anim.SetInteger("Transition", (int)state.AnimationState);
    }

    private void CalculateScore()
    {
        score = Math.Max(score, (int)(transform.position.x - startX));
        scoreText.text = score.ToString();
    }

    private void UpdateHealthUI()
    {
        healthText.text = heroState.Health.ToString();
    }
}
