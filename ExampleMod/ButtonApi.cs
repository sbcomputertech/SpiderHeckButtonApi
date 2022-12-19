using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ButtonApiMod;

public static class ButtonApi
{
	public struct ButtonInfo
	{
		public string text;
		public UnityAction onClick;
		public Button buttonObj;
		public bool added;
	}

	public static List<ButtonInfo> buttons = new();
	public static void RegisterButton(string text, UnityAction onClick)
	{
		var bi = new ButtonInfo
		{
			text = text,
			onClick = onClick
		};
		buttons.Add(bi);
	}
}