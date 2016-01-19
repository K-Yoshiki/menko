using UnityEngine;
using UnityEngine.UI;

namespace AppUtils.MVVM
{
	[AddComponentMenu("MVVM/Components/View Scrollbar")]
	[RequireComponent(typeof(Scrollbar))]
	public class ViewScrollbar : View
	{
		[Header("Property Names")]
		[SerializeField] string enabledName;
		[SerializeField] string interactableName;
		[SerializeField] string valueName;
		[SerializeField] string sizeName;
		[SerializeField] string stepsName;
		Scrollbar uiScrollbar;

		protected override void Init()
		{
			uiScrollbar = GetComponent<Scrollbar>();
			uiScrollbar.onValueChanged.AddListener(OnValueChanged);
			AddUpdater(enabledName, UpdateEnabled);
			AddUpdater(interactableName, UpdateInteractable);
			AddUpdater(valueName, UpdateValue);
			AddUpdater(sizeName, UpdateSize);
			AddUpdater(stepsName, UpdateStep);
		}

		void OnValueChanged(float value)
		{
			SetValue(valueName, value);
		}

		void UpdateEnabled(object value)
		{
			uiScrollbar.enabled = (bool)value;
		}

		void UpdateInteractable(object value)
		{
			uiScrollbar.interactable = (bool)value;
		}

		void UpdateValue(object value)
		{
			uiScrollbar.value = (float)value;
        }

		void UpdateSize(object value)
		{
			uiScrollbar.size = (float)value;
        }

		void UpdateStep(object value)
		{
			uiScrollbar.numberOfSteps = (int)value;
        }
	}
}