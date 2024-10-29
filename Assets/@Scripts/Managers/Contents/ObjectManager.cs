using static Define;

using UnityEngine;
using System.Collections.Generic;

public class ObjectManager
{
	private HashSet<Hero> heroes = new();
	private Queue<Catnip> catnips = new();
	private Queue<ItemBox> itemBoxes = new();
	private Dictionary<string, Queue<Monster>> monsters = new();

	#region Roots
	public Transform GetRootTransform(string name)
	{
		GameObject root = GameObject.Find(name);
		if (root == null)
			root = new GameObject { name = name };

		return root.transform;
	}

	public Transform root { get { return GetRootTransform("@UI_Root"); } }
	public Transform heroRoot { get { return GetRootTransform("@Heroes"); } }
	public Transform monsterRoot { get { return GetRootTransform("@Monsters"); } }
	#endregion

	public void Init()
	{
		foreach (var key in Managers.Data.Monsters.Keys)
			monsters.Add(key, new Queue<Monster>());
	}

	public Hero SpawnHero(Vector3 position)
	{
		string name = "Hero";
		GameObject go = Managers.Resource.Instantiate(name, heroRoot);
		go.name = name;
		go.transform.localScale = new Vector3(0.22f, 0.22f, 0.22f);
		go.transform.position = position;

		Hero obj = go.GetComponent<Hero>();
		return obj;
	}

	public Monster SpawnMonster(Vector3 position, string id)
	{
		Monster obj = GetMonster(id);

		obj.transform.localScale = new Vector3(0.22f, 0.22f, 0.22f);
		obj.transform.position = position;
		obj.gameObject.SetActive(true);

		monsters[id].Enqueue(obj);
		return obj;
	}

	public Catnip SpawnCatnip(Vector3 position)
	{
		Catnip obj = GetCatnip();

		obj.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
		obj.transform.position = position;
		obj.gameObject.SetActive(true);

		catnips.Enqueue(obj);
		return obj;
	}

	public ItemBox SpawnItem(Vector3 position)
	{
		ItemBox obj = GetItemBox();

		obj.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
		obj.transform.position = position;
		obj.gameObject.SetActive(true);

		itemBoxes.Enqueue(obj);
		return obj;
	}

	public void Despawn<T>(T obj) where T : BaseObject
	{
		if (obj.ObjectType == EObjectType.Hero)
		{
			Hero heroObj = obj as Hero;
			heroes.Remove(heroObj);
		}

		Managers.Resource.Destroy(obj.gameObject);
	}

	private Monster GetMonster(string id)
	{
		if (monsters[id].Count <= 0 || !monsters[id].Peek().IsScreenPassed())
		{
			string name = $"Monster{id}";
			GameObject go = Managers.Resource.Instantiate(name, monsterRoot);
			go.name = name;
			return go.GetComponent<Monster>();
		}
		else
			return monsters[id].Dequeue();
	}

	private Catnip GetCatnip()
	{
		if (catnips.Count <= 0 || !catnips.Peek().IsScreenPassed())
		{
			string name = "Catnip";
			GameObject go = Managers.Resource.Instantiate(name, monsterRoot);
			go.name = name;
			return go.GetComponent<Catnip>();
		}
		else
			return catnips.Dequeue();
	}

	private ItemBox GetItemBox()
	{
		if (itemBoxes.Count <= 0 || !itemBoxes.Peek().IsScreenPassed())
		{
			string name = "ItemBox";
			GameObject go = Managers.Resource.Instantiate(name, monsterRoot);
			go.name = name;
			return go.GetComponent<ItemBox>();
		}
		else
			return itemBoxes.Dequeue();
	}

	public void ClearMonster()
	{
		foreach (KeyValuePair<string, Queue<Monster>> queue in monsters)
			while (queue.Value.Count > 0)
			{
				Monster monsterObj = queue.Value.Dequeue();
				Managers.Resource.Destroy(monsterObj.gameObject);
			}
	}

	public void ClearCatnip()
	{
		while (catnips.Count > 0)
		{
			Catnip catnipObj = catnips.Dequeue();
			Managers.Resource.Destroy(catnipObj.gameObject);
		}
	}

	public void ClearItemBox()
	{
		while (itemBoxes.Count > 0)
		{
			ItemBox itemBoxObj = itemBoxes.Dequeue();
			Managers.Resource.Destroy(itemBoxObj.gameObject);
		}
	}
}
