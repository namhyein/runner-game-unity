/* 프로젝트에 사용되는 상수들을 정의하는 스크립트 */
using System;
using System.Collections.Generic;
using UnityEngine;

public static class Theme
{
	public enum Padding
	{
		S50 = 2,
		S100 = 4,
		S150 = 6,
		S200 = 8,
		S250 = 10,
		S300 = 12,
		S400 = 16,
		S450 = 18,
		S500 = 20,
		S550 = 22,
		S600 = 24,
		S700 = 28,
		S800 = 32,
		S1200 = 48,
	}

	public enum Spacing
	{

		S50 = 2,
		S100 = 4,
		S150 = 6,
		S200 = 8,
		S250 = 10,
		S300 = 12,
		S400 = 16,
		S500 = 20,
		S600 = 24,
		S700 = 28,
		S800 = 32,
	}

	public enum Radus
	{
		S100 = 4,
		S150 = 6,
		S200 = 8,
		S300 = 12,
		S400 = 16,
		S500 = 20,
	}
}

public static class ColorTheme
{
	public static readonly Color32 RedLight01 = new(0xFF, 0xF2, 0xEE, 0xFF);
	public static readonly Color32 RedLight02 = new(0xFF, 0x6F, 0x6F, 0xFF);
	public static readonly Color32 RedOrange = new(0xFF, 0x46, 0x1D, 0xFF);
	public static readonly Color32 RedDefault = new(0xCC, 0x25, 0x00, 0xFF);

	public static readonly Color32 BlueLight01 = new(0xD2, 0xDF, 0xFF, 0xFF);
	public static readonly Color32 BlueLight02 = new(0x73, 0xA3, 0xFF, 0xFF);
	public static readonly Color32 BlueMoon = new(0x6E, 0x83, 0xF4, 0xFF);
	public static readonly Color32 BlueDefault = new(0x43, 0x55, 0xFF, 0xFF);

	public static readonly Color32 GreenLight = new(0xE7, 0xEA, 0xCF, 0xFF);
	public static readonly Color32 GreenStar = new(0x2C, 0xE8, 0xA4, 0xFF);
	public static readonly Color32 GreenBright = new(0x48, 0xFF, 0x86, 0xFF);
	public static readonly Color32 GreenDefault = new(0x4F, 0xAA, 0x88, 0xFF);
	public static readonly Color32 GreenDark = new(0x39, 0x86, 0x69, 0xFF);

	public static readonly Color32 YellowLight = new(0xFD, 0xF6, 0xDE, 0xFF);
	public static readonly Color32 YellowDefault = new(0xFF, 0xD8, 0x54, 0xFF);
	public static readonly Color32 YellowStar = new(0xFF, 0xB0, 0x39, 0xFF);
	public static readonly Color32 YellowBright = new(0xF4, 0xFD, 0x79, 0xFF);

	public static readonly Color32 Purple = new(0x7B, 0x59, 0xFF, 0xFF);
	public static readonly Color32 PurpleWhite = new(0xF9, 0xF3, 0xFF, 0xFF);
	public static readonly Color32 PurpleDefault = new(0x7B, 0x59, 0xFF, 0xFF);
	public static readonly Color32 PurpleDark01 = new(0x4B, 0x31, 0xB1, 0xFF);
	public static readonly Color32 PurpleDark02 = new(0x3C, 0x28, 0x8C, 0xFF);
	public static readonly Color32 PurpleLight01 = new(0xDA, 0xB1, 0xFF, 0xFF);
	public static readonly Color32 PurpleLight02 = new(0x95, 0x7A, 0xFF, 0xFF);

	public static readonly Color32 GreyBlack = new(0x19, 0x19, 0x19, 0xFF);
	public static readonly Color32 GreyWhite = new(0xF3, 0xF3, 0xF3, 0xFF);
	public static readonly Color32 GreyLight = new(0xDF, 0xDF, 0xDF, 0xFF);
	public static readonly Color32 Grey03 = new(0xC8, 0xC8, 0xC8, 0xFF);
	public static readonly Color32 Grey02 = new(0x74, 0x74, 0x74, 0xFF);
	public static readonly Color32 Grey01 = new(0x60, 0x60, 0x60, 0xFF);
}