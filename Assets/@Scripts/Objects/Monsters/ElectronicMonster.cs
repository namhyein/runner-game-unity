using UnityEngine;

public class ElectronicMonster : Monster
{
    private float lastSkillTime;
    private float skillCooldown;

    void Update()
    {
        Vector3 heroPosition = Managers.Game.GetHeroPosition();
        if (heroPosition.y + 8 < transform.position.y || heroPosition.y > transform.position.y) return;
        if (Time.time - lastSkillTime >= skillCooldown)
            Skill();
    }

    void Skill()
    {
        Attack();

        float randomX = Random.Range(-2, 3);
        float randomY = Random.Range(-1, -4);

        Transform target = GameObject.Find("@Monsters").transform;
        GameObject go = Managers.Resource.Instantiate("ElectronicSkill", target);
        go.transform.position = new(randomX, transform.position.y + randomY + 0.5f, 0);
        go.transform.localScale = new(0.5f, 0.5f, 0.5f);

        lastSkillTime = Time.time;
        skillCooldown = Random.Range(2f, 4f);
    }
}