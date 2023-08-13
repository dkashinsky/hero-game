using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;

public enum HeroAnimationStateEnum
{
    Stand = 1,
    Run = 2,
    Jump = 3,
    Fall = 4
}

public interface IHeroAnimationContext
{
    void SetHeroState(AnimatedHeroState state);
}

public abstract class HeroState
{
    public int Health { get; protected set; }
    public int MaxHealth { get; protected set; }

    public bool IsAlive => Health > 0;
    public bool IsHealthy => Health == MaxHealth;

    public virtual void UpdateHealth(int delta)
    { 
        Health += delta;

        if (Health > MaxHealth)
            Health = MaxHealth;

        if (Health < 0)
            Health = 0;
    }
}

public abstract class AnimatedHeroState : HeroState
{
    public IHeroAnimationContext HeroContext { get; protected set; }
    public HeroAnimationStateEnum AnimationState { get; protected set; }
    public bool IsMoving { get; protected set; }
    public bool IsJumping { get; protected set; }
    public virtual void UpdateIsMoving(bool isMoving) { }
    public virtual void UpdateIsJumping(bool isJumping) { }

    public AnimatedHeroState()
    {
    }

    public AnimatedHeroState(AnimatedHeroState prevState)
    {
        HeroContext = prevState.HeroContext;

        Health = prevState.Health;
        MaxHealth = prevState.MaxHealth;
        IsMoving = prevState.IsMoving;
        IsJumping = prevState.IsJumping;
    }

    public AnimatedHeroState(AnimatedHeroState prevState, HeroAnimationStateEnum animationState) : this(prevState)
    {
        AnimationState = animationState;
    }

    public override void UpdateHealth(int delta)
    {
        base.UpdateHealth(delta);

        if (!IsAlive)
            HeroContext.SetHeroState(new DeadState(this));
    }
}

public class StandingState : AnimatedHeroState
{
    public StandingState(AnimatedHeroState prevState): base(prevState, HeroAnimationStateEnum.Stand) { }

    public StandingState(IHeroAnimationContext context)
    {
        Health = 100;
        MaxHealth= 100;
        HeroContext = context;
        AnimationState = HeroAnimationStateEnum.Stand;
    }

    public override void UpdateIsJumping(bool isJumping)
    {
        if (IsJumping != isJumping)
        {
            // Jumped while standing
            IsJumping = isJumping;
            HeroContext.SetHeroState(new JumpingState(this));
        }
    }

    public override void UpdateIsMoving(bool isMoving)
    {
        if (IsMoving != isMoving)
        {
            // moved while standing
            IsMoving = isMoving;
            HeroContext.SetHeroState(new MovingState(this));
        }
    }
}

public class MovingState : AnimatedHeroState
{
    public MovingState(AnimatedHeroState prevState) : base(prevState, HeroAnimationStateEnum.Run) { }

    public override void UpdateIsMoving(bool isMoving)
    {
        if (IsMoving != isMoving)
        {
            // stopped moving
            IsMoving = isMoving;
            HeroContext.SetHeroState(new StandingState(this));
        }
    }

    public override void UpdateIsJumping(bool isJumping)
    {
        if (IsJumping != isJumping)
        {
            // jumped while moving
            IsJumping = isJumping;
            HeroContext.SetHeroState(new JumpingState(this));
        }
    }
}

public class JumpingState : AnimatedHeroState
{
    public JumpingState(AnimatedHeroState prevState) : base(prevState, HeroAnimationStateEnum.Jump) { }

    public override void UpdateIsMoving(bool isMoving)
    {
        IsMoving = isMoving;
    }

    public override void UpdateIsJumping(bool isJumping)
    {
        if (IsJumping != isJumping)
        {
            IsJumping = isJumping;

            if (IsMoving) // landed while moving
                HeroContext.SetHeroState(new MovingState(this));
            else // landed while standing still
                HeroContext.SetHeroState(new StandingState(this));
        }
    }
}

public class DeadState : AnimatedHeroState
{
    public DeadState(AnimatedHeroState prevState) : base(prevState, HeroAnimationStateEnum.Fall) { }
}