using UnityEngine;

public class FireMonster : Monster
{
    private float skillCooldown = 10;
    private float lastSkillTime;

    void Update()
    {
        if (Camera.main.transform.position.y > transform.position.y) return;
        if (Camera.main.transform.position.y + Camera.main.orthographicSize < transform.position.y) return;

        if (Time.time - lastSkillTime >= skillCooldown)
            Skill();
    }

    void Skill()
    {
        Attack();

        Transform target = GameObject.Find("@Monsters").transform;
        GameObject go = Managers.Resource.Instantiate("FireSkill", target);

        go.transform.position = transform.position;

        lastSkillTime = Time.time;
        skillCooldown = Random.Range(2f, 4f);
    }
}