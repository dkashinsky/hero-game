using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class HeroManager : MonoBehaviour, IHeroAnimationContext
{
    public FixedJoystick fixedJoystick;
    public GameResultScriptableObject gameResult;
    public Text scoreText;
    public Text healthText;
    public AudioSource jumpSound;
    public AudioSource dieSound;
    private float jumpForce = 7.0f;      // Amount of force added when the player jumps.
    private float movementSpeed = 7.0f; // The speed of the horizontal movement
    private float startX = 0f;
    private float joystickThreshold = 0.5f;
    private Animator anim;
    private Rigidbody2D body;
    private AnimatedHeroState heroState;

    private bool IsJumpPressed()
    {
        return fixedJoystick.gameObject.activeSelf
            ? fixedJoystick.Vertical >= joystickThreshold
            : Input.GetKeyDown(KeyCode.UpArrow);
    }

    private bool IsLeftPressed()
    {
        return fixedJoystick.gameObject.activeSelf
            ? fixedJoystick.Horizontal <= -joystickThreshold
            : Input.GetKey(KeyCode.LeftArrow);
    }

    private bool IsRightPressed()
    {
        return fixedJoystick.gameObject.activeSelf
            ? fixedJoystick.Horizontal >= joystickThreshold
            : Input.GetKey(KeyCode.RightArrow);
    }

    private bool IsMovementReleased()
    {
        return fixedJoystick.gameObject.activeSelf
            ? (fixedJoystick.Horizontal > -joystickThreshold && fixedJoystick.Horizontal < joystickThreshold)
            : (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow));
    }

    void Awake()
    {
        fixedJoystick.gameObject.SetActive(Application.platform == RuntimePlatform.Android);
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        SetHeroState(new StandingState(this));
        startX = transform.position.x;
        gameResult.score = 0;
    }

    void Update()
    {
        if (heroState.IsAlive)
        {
            if (IsJumpPressed() && !heroState.IsJumping)
            {
                // Add a vertical force to the player.
                body.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                heroState.UpdateIsJumping(true);
                jumpSound.Play();
            }

            if (IsRightPressed())
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
                transform.position = new Vector3(
                    transform.position.x + Time.deltaTime * movementSpeed,
                    transform.position.y,
                    0f);
                heroState.UpdateIsMoving(true);
            }
            else if (IsLeftPressed())
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
                transform.position = new Vector3(
                    transform.position.x - Time.deltaTime * movementSpeed,
                    transform.position.y,
                    0f);
                heroState.UpdateIsMoving(true);
            }

            if (IsMovementReleased())
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

        if (!heroState.IsAlive)
        {
            dieSound.Play();
            StartCoroutine(EndGameAfterSeconds(3));
        }
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
    }

    IEnumerator EndGameAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        gameResult.isWin = false;
        ScenesManager.Instance.EndGame();
    }
}
