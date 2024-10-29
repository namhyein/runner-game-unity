using UnityEngine;
using static Define;


public class Catnip : BaseObject
{
  protected override bool Init()
  {
    if (base.Init() == false)
      return false;

    ObjectType = EObjectType.Catnip;
    return true;
  }

  void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.gameObject.CompareTag("Player"))
    {
      if (collision.gameObject.GetComponent<Hero>().ObjectState != EState.Jump)
        gameObject.SetActive(false);
    }
  }

  void Update()
  {
    if (transform.position.y < Camera.main.transform.position.y - Camera.main.orthographicSize * 2)
      Destroy(gameObject);
  }
}
