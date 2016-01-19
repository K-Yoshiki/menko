using UnityEngine;
using UnityEngine.UI;

namespace AppUtils.MVVM
{
	/// <summary>
	/// テキストのビュークラス
	/// </summary>
	[AddComponentMenu("MVVM/Components/View Text")]
	[RequireComponent(typeof(Text))]
	public class ViewText : View
	{
		[Header("Property Names")]
		[SerializeField] string enabledName;
		[SerializeField] string textName;
		[SerializeField] string colorName;
		[Header("Other Settings")]
		[SerializeField, TextArea(2, 5)] string textFormat;
		Text uiText;

		protected override void Init()
		{
			uiText = GetComponent<Text>();
			AddUpdater(enabledName, UpdateEnabled);
			AddUpdater(textName, UpdateText);
			AddUpdater(colorName, UpdateColor);
		}

		void UpdateEnabled(object value)
		{
			uiText.enabled = (bool)value;
		}

		void UpdateText(object value)
		{
			string str = (string)value;
			if (textFormat != "")
			{
				str = string.Format(textFormat, str);
			}
			uiText.text = str;
		}

		void UpdateColor(object value)
		{
			uiText.color = (Color)value;
		}
	}
}