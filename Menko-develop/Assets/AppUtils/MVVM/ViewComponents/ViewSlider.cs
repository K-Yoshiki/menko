using UnityEngine;
using UnityEngine.UI;

namespace AppUtils.MVVM
{
	[AddComponentMenu("MVVM/Components/View Slider")]
	[RequireComponent(typeof(Slider))]
	public class ViewSlider : View
	{
		[Header("Property Names")]
		[SerializeField] string enabledName;
		[SerializeField] string interactableName;
		[SerializeField] string minValueName;
		[SerializeField] string maxValueName;
		[SerializeField] string valueName;
		Slider uiSlider;

		protected override void Init()
		{
			uiSlider = GetComponent<Slider>();
			uiSlider.onValueChanged.AddListener(OnValueChanged);
			AddUpdater(enabledName, UpdateEnabled);
			AddUpdater(interactableName, UpdateInteractable);
			AddUpdater(minValueName, UpdateMinValue);
			AddUpdater(maxValueName, UpdateMaxValue);
			AddUpdater(valueName, UpdateValue);
		}

		void OnValueChanged(float value)
		{
			SetValue(valueName, value);
		}

		void UpdateEnabled(object value)
		{
			uiSlider.enabled = (bool)value;
		}

		void UpdateInteractable(object value)
		{
			uiSlider.interactable = (bool)value;
		}

		void UpdateMinValue(object value)
		{
			uiSlider.minValue = (float)value;
        }

		void UpdateMaxValue(object value)
		{
			uiSlider.maxValue = (float)value;
        }

		void UpdateValue(object value)
		{
			uiSlider.value = (float)value;
        }
	}
}