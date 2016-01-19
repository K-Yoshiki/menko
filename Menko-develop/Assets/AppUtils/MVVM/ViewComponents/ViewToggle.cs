using UnityEngine;
using UnityEngine.UI;

namespace AppUtils.MVVM
{
	/// <summary>
	/// トグル要素のビュークラス
	/// </summary>
	[AddComponentMenu("MVVM/Components/View Toggle")]
	[RequireComponent(typeof(Toggle))]
	public class ViewToggle : View
	{
		[Header("Property Names")]
		[SerializeField] string enabledName;
		[SerializeField] string interactableName;
		[SerializeField] string toggleName;
		Toggle uiToggle;

		protected override void Init()
		{
			uiToggle = GetComponent<Toggle>();
			uiToggle.onValueChanged.AddListener(OnChangeValue);
			AddUpdater(enabledName, UpdateEnabled);
			AddUpdater(interactableName, UpdateInteractive);
			AddUpdater(toggleName, UpdateToggle);
		}

		void OnChangeValue(bool value)
		{
			SetValue(toggleName, value);
		}

		void UpdateEnabled(object value)
		{
			uiToggle.enabled = (bool)value;
		}

		void UpdateInteractive(object value)
		{
			uiToggle.interactable = (bool)value;
		}

		void UpdateToggle(object value)
		{
			uiToggle.isOn = (bool)value;
		}

		void OnDestory()
		{
			uiToggle.onValueChanged.RemoveListener(OnChangeValue);
		}
	}
}