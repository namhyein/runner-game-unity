using UnityEngine;
using System.Collections;

using static Define;

public class Monster : Creature
{
    protected override void Initialize()
    {
        base.Initialize();

        ObjectType = EObjectType.Monster;
    }

    void Start()
    {
        Idle();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        Hero hero = collision.gameObject.GetComponent<Hero>();

        if (hero.ObjectState == EState.Jump) Damaged();
        else if (hero.ObjectState == EState.Invincible) Dead();
        else Attack(hero.ObjectState);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;
        Idle();
    }

    public void Attack(EState collisionState)
    {
        Attack();
        if (collisionState != EState.Dead) StartCoroutine(DestroyMonster());
    }

    private IEnumerator DestroyMonster()
    {
        yield return new WaitForSeconds(1.0f);
        gameObject.SetActive(false);
    }
}
