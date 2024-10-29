using Ingame;
using UnityEngine;
using static Define;


public class ItemBox : BaseObject
{
  public Item item;
  protected override bool Init()
  {
    if (base.Init() == false)
      return false;

    ObjectType = EObjectType.ItemBox;
    SelectItem();

    return true;
  }

  void SelectItem()
  {

  }

  void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.gameObject.CompareTag("Player")) gameObject.SetActive(false);
  }
}
