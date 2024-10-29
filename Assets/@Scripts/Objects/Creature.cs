using System;
using static Define;

public class Creature : BaseObject
{
	protected AnimatorController animator;

	private EState _objectState;
	public EState ObjectState
	{
		get { return _objectState; }
		set
		{
			if (_objectState == value)
				return;

			_objectState = value;
			OnStateChanged?.Invoke(value);
		}
	}
	public Action<EState> OnStateChanged;

	protected override bool Init()
	{
		if (base.Init() == false)
			return false;

		Initialize();

		return true;
	}

	protected virtual void Initialize()
	{
		animator = transform.Find("SpineAnim").GetComponent<AnimatorController>();
	}

	public virtual void Idle()
	{
		if (ObjectState == EState.Dead) return;
		animator.Idle();
	}

	public virtual void Dead()
	{
		if (ObjectState == EState.Dead) return;
		animator.Dead();
	}

	public virtual void Attack()
	{
		if (ObjectState == EState.Dead) return;
		animator.Attack();
	}

	public virtual void Damaged()
	{
		if (ObjectState == EState.Dead) return;
		animator.Damaged();
	}

	public virtual void Move()
	{
		if (ObjectState == EState.Dead) return;
		animator.Move();
	}

	public virtual void Jump()
	{
		if (ObjectState == EState.Dead) return;
		animator.Jump();
	}
}