using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace AppUtils.MVVM
{
	/// <summary>
	/// ドロップダウン要素のビュークラス
	/// </summary>
	[AddComponentMenu("MVVM/Components/View Dropdown")]
	[RequireComponent(typeof(Dropdown))]
	public class ViewDropdown : View
	{
		[Header("Property Names")]
		[SerializeField] string enabledName;
		[SerializeField] string interactableName;
		[SerializeField] string captionListName;
		[SerializeField] string selectNumName;
		Dropdown uiDropdown;
		int selectNum;

		protected override void Init()
		{
			uiDropdown = GetComponent<Dropdown>();
			uiDropdown.onValueChanged.AddListener(OnChangeId);
			AddUpdater(enabledName, UpdateEnabled);
			AddUpdater(interactableName, UpdateInteractable);
			AddUpdater(selectNumName, UpdateSelectNumber);
			AddUpdater(captionListName, UpdateCaptionList);
		}

		void OnChangeId(int value)
		{
			SetValue(selectNumName, value);
		}

		void UpdateEnabled(object value)
		{
			uiDropdown.enabled = (bool)value;
		}

		void UpdateInteractable(object value)
		{
			uiDropdown.interactable = (bool)value;
		}

		void UpdateSelectNumber(object value)
		{
			uiDropdown.value = selectNum = (int)value;
		}

		void UpdateCaptionList(object value)
		{
			List<string> list = (List<string>)value;
			List<Dropdown.OptionData> options = uiDropdown.options;
			options.Clear();
			for (int i = 0; i < list.Count; ++i)
			{
				options.Add(new Dropdown.OptionData(list[i]));
			}
			uiDropdown.options = options;
			uiDropdown.value = selectNum;
		}
	}
}