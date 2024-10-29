using System;
using UnityEngine;
using static Define;

public class AnimatorController : InitBase
{
  public Animator animator;
  private Creature creature;

  protected override bool Init()
  {
    if (base.Init() == false)
      return false;

    animator = GetComponent<Animator>();
    creature = GetComponentInParent<Creature>();
    return true;
  }

  public void Idle()
  {
    animator.SetInteger("state", (int)EState.Idle);
    creature.ObjectState = EState.Idle;
  }

  public void Dead()
  {
    animator.SetInteger("state", (int)EState.Dead);
    creature.ObjectState = EState.Dead;
  }

  public void Attack()
  {
    animator.SetInteger("state", (int)EState.Attack);
    creature.ObjectState = EState.Attack;
  }

  public void Damaged()
  {
    animator.SetInteger("state", (int)EState.Damaged);
    creature.ObjectState = EState.Damaged;
  }

  public void Move()
  {
    animator.SetInteger("state", (int)EState.Move);
    creature.ObjectState = EState.Move;
  }

  public void Jump()
  {
    animator.SetInteger("state", (int)EState.Jump);
    creature.ObjectState = EState.Jump;
  }
}
