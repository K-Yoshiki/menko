using UnityEngine;
using UnityEngine.UI;

namespace AppUtils.MVVM
{
	/// <summary>
	/// イメージのビュークラス
	/// </summary>
	[AddComponentMenu("MVVM/Components/View Image")]
	[RequireComponent(typeof(Image))]
	public class ViewImage : View
	{
		[Header("Property Names")]
		[SerializeField] string enabledName;
		[SerializeField] string spriteName;
		[SerializeField] string colorName;
		[SerializeField] string materialName;
		Image uiImage;

		protected override void Init()
		{
			uiImage = GetComponent<Image>();
			AddUpdater(enabledName, UpdateEnabled);
			AddUpdater(spriteName, UpdateSprite);
			AddUpdater(colorName, UpdateColor);
			AddUpdater(materialName, UpdateMaterial);
		}

		void UpdateEnabled(object value)
		{
			uiImage.enabled = (bool)value;
		}

		void UpdateSprite(object value)
		{
			if (value != null)
			{
				uiImage.sprite = (Sprite)value;
			}
		}

		void UpdateColor(object value)
		{
			uiImage.color = (Color)value;
		}

		void UpdateMaterial(object value)
		{
			if (value != null)
				uiImage.material = (Material)value;
		}
	}
}