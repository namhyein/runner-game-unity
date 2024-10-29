using UnityEngine;

public class BaseObject : InitBase
{
	public Define.EObjectType ObjectType { get; protected set; } = Define.EObjectType.None;

	protected override bool Init()
	{
		if (base.Init() == false)
			return false;

		return true;
	}

	public bool IsScreenPassed()
	{
		return transform.position.y < Camera.main.transform.position.y - Camera.main.orthographicSize * 2;
	}
}
