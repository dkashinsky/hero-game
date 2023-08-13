using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class HeroManager : MonoBehaviour, IHeroAnimationContext
{
    public float jumpForce = 7.0f;      // Amount of force added when the player jumps.
    private float movementSpeed = 0.045f; // The speed of the horizontal movement
    private Animator anim;
    private Rigidbody2D body;
    private AnimatedHeroState heroState;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        SetHeroState(new StandingState(this));
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
                    transform.position.x + movementSpeed,
                    transform.position.y,
                    0f);
                heroState.UpdateIsMoving(true);
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
                transform.position = new Vector3(
                    transform.position.x - movementSpeed,
                    transform.position.y,
                    0f);
                heroState.UpdateIsMoving(true);
            }

            if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
            {
                heroState.UpdateIsMoving(false);
            }
        }
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
}
