public static class Define
{
	public enum ETheme
	{
		Palace = 1,
		Waterway = 2,
		Garden = 3,
		Forest = 4,
		Temple = 5
	}

	public enum EAdType
	{
		Minute,
		GameOver,
		Diamond,
		Coin,
		Potion,
		Heart,
		Quest
	}

	public enum EScene
	{
		Unknown,
		SplashScene,
		LobbyScene,
		GameScene,
		RankingScene
	}

	public enum EUIEvent
	{
		Click,
		PointerDown,
		PointerUp,
		Drag,
	}

	public enum ETouchPadState
	{
		PointerDown,
		PointerUp,
		Drag,
	}

	public enum ESound
	{
		Bgm,
		Sfx,
		Max, // Sound 종류의 개수
	}

	public enum EObjectType
	{
		None,
		Hero,
		Monster,
		Obstacle,
		ItemBox,
		Catnip
	}

	/* Hero & Monster animator의 state 변수와 순서 동일해야함 */
	public enum EState
	{
		None = 0,
		Idle = 1,
		Move = 2,
		Attack = 3,
		Damaged = 4,
		Jump = 5,
		Dead = 6,
		Invincible = 7,
	}

	public enum EWeaponType
	{
		None,
		Spear,
		Arrow,
		Orb,
		Laser,
	}

	public enum EMonsterType
	{
		None,
		Mob_Default,
		Mob_Wood,
		Mob_SteelArmor,
		Mob_SteelHat,
		Mob_Gold,
		Mob_Ruby,
		Mob_Diamond,
	}

	public enum EEventAreaType
	{
		None,
		EventArea_1,
		EventArea_2,
	}

	public enum EEventType
	{
		StoreEvent,
		RandomEvent,
		Card,
		HealPotion_15,
		HealPotion_30,
		Jelly_5,
		Jelly_6,
		Jelly_7,
	}

	public enum EDataTag
	{
		PreLoad,
		Ingame,
		Outgame,
	}

	public enum EItemType
	{
		None,
		Diamond = 1,
		Coin = 2,
		Catnip = 3,
		WpStone = 4,
		Heart = 5,
		Jelly = 6,
	}
}

public static class InGameDef
{
	#region Map / Chapter / CheckPoint
	public const int MAP_WIDTH = 7; // 맵의 너비 (카메라에 보여지는 타일의 가로 개수)

	public const float CHKPOINT_1 = 0.33f;
	public const float CHKPOINT_2 = 0.66f;
	public const float CHKPOINT_3 = 1.0f;
	#endregion

	#region Prefab
	public const string HERO_PREFAB = "Hero";
	#endregion
}
