using System.Collections;
using UnityEngine;
using static Define;


public class Hero : Creature
{
    public bool isDamaged = false;

    public bool IsMovable
    {
        get { return ObjectState == EState.Move || ObjectState == EState.Jump || ObjectState == EState.Invincible; }
    }

    void Start()
    {
        Move();
        OnStateChanged += (state) =>
        {
            if ((state == EState.Move) && isDamaged)
                StartCoroutine(DamagedCoroutine());
        };
    }

    private IEnumerator DamagedCoroutine()
    {
        yield return new WaitForSeconds(1.0f);
        isDamaged = false;
    }

    protected override void Initialize()
    {
        base.Initialize();

        ObjectType = EObjectType.Hero;
    }

    #region Collision
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Monster") && ObjectState == EState.Move)
        {
            if (Managers.Game.CheckifDie())
                Dead();
            else
            {
                Damaged();
            }
        };
        if (collision.gameObject.CompareTag("Catnip") && ObjectState != EState.Jump) EarnCoin();
    }
    #endregion

    public override void Jump()
    {
        if (ObjectState != EState.Move) return;
        base.Jump();
        transform.Find("Effect_Jump").GetComponent<ParticleSystem>().Play();
    }

    public override void Damaged()
    {
        if (!IsMovable || isDamaged) return;

        isDamaged = true;
        base.Damaged();
        LoseCoin();
        transform.Find("Effect_Lose").GetComponent<ParticleSystem>().Play();
    }

    public void MoveTo(Vector3 targetPosition)
    {
        if (ObjectState == EState.Dead || ObjectState == EState.Idle) return;
        if (ObjectState != EState.Move && ObjectState != EState.Jump && ObjectState != EState.Invincible) targetPosition.y = 0;

        if (ObjectState == EState.Jump) targetPosition.y *= 0.9f;
        if (targetPosition.x < -2) targetPosition.x = -2;
        else if (targetPosition.x > 2) targetPosition.x = 2;

        transform.Translate(targetPosition);
    }

    private void EarnCoin()
    {
        Managers.Game.EarnCoin();
        transform.Find("Effect_Earn").GetComponent<ParticleSystem>().Play();
    }

    private void LoseCoin()
    {
        Managers.Game.LoseCoin();
        transform.Find("Effect_Lose").GetComponent<ParticleSystem>().Play();
    }
}
