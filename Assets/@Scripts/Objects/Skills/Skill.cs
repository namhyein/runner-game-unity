using UnityEngine;

public class Skill : BaseObject
{
  void Update()
  {
    if (IsScreenPassed())
      Destroy(gameObject);
  }

  void OnParticleCollision(GameObject other)
  {
    if (other.CompareTag("Player"))
    {
      if (Managers.Game.CheckifDie())
        other.GetComponent<Hero>().Dead();
      else
        other.GetComponent<Hero>().Damaged();
    }
  }
}