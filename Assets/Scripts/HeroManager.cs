using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class HeroManager : MonoBehaviour, IHeroAnimationContext
{
    public GameResultScriptableObject gameResult;
    public Text scoreText;
    public Text healthText;
    private float jumpForce = 7.0f;      // Amount of force added when the player jumps.
    private float movementSpeed = 7.0f; // The speed of the horizontal movement
    private float startX = 0f;
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
        gameResult.score = 0;
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
        if (collision.gameObject.tag == KnownGameObjects.Level)
        {
            heroState.UpdateIsJumping(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == KnownGameObjects.Finish)
        {
            gameResult.isWin = true;
            ScenesManager.Instance.EndGame();
        }
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
        var score = gameResult.score;
        score = Math.Max(score, (int)(transform.position.x - startX));

        scoreText.text = score.ToString();
        gameResult.score = score;
    }

    private void UpdateHealthUI()
    {
        healthText.text = heroState.Health.ToString();

        if (!heroState.IsAlive)
        {
            StartCoroutine(EndGameAfterSeconds(3));
        }
    }

    IEnumerator EndGameAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        gameResult.isWin = false;
        ScenesManager.Instance.EndGame();
    }
}
