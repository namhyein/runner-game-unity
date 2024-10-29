using UnityEngine;

public class MeteorMonster : Monster
{
    private float skillCooldown = 10;
    private float lastSkillTime;

    void Update()
    {
        Vector3 heroPosition = Managers.Game.GetHeroPosition();
        if (heroPosition.y + 8 < transform.position.y || heroPosition.y > transform.position.y) return;
        if (Time.time - lastSkillTime >= skillCooldown)
        {
            Skill();
            lastSkillTime = Time.time;
            skillCooldown = Random.Range(7f, 4f);
        }
    }

    void Skill()
    {
        Attack();

        Transform target = GameObject.Find("@Monsters").transform;
        GameObject go = Managers.Resource.Instantiate("MeteorSkill", target);

        go.transform.position = new(transform.position.x, transform.position.y - 0.5f, 0);
    }
}