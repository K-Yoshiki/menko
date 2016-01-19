using UnityEngine;
using UnityEngine.UI;

namespace AppUtils.MVVM
{
	/// <summary>
	/// 入力用テキストフィールドのビュークラス
	/// </summary>
	[AddComponentMenu("MVVM/Components/View Input Field")]
	[RequireComponent(typeof(InputField))]
	public class ViewInputField : View
	{
		[Header("Property Names")]
		[SerializeField] string enabledName;
		[SerializeField] string interactableName;
		[SerializeField] string textName;
		[SerializeField] string charLimitName;
		[Header("Other Settings")]
		[SerializeField] bool isUpdateChanged;
		InputField uiInputField;

		protected override void Init()
		{
			uiInputField = this.GetComponent<InputField>();
			AddUpdater(enabledName, UpdateEnabled);
			AddUpdater(interactableName, UpdateInteractable);
			AddUpdater(charLimitName, UpdateCharLimit);

			if (isUpdateChanged)
			{
				uiInputField.onValueChange.AddListener(UpdateText);
			}
			else
			{
				uiInputField.onEndEdit.AddListener(UpdateText);
			}
		}

		private void UpdateEnabled(object value)
		{
			uiInputField.enabled = (bool)value;
		}

		private void UpdateInteractable(object value)
		{
			uiInputField.interactable = (bool)value;
		}

		void UpdateCharLimit(object value)
		{
			uiInputField.characterLimit = (int)value;
		}

		void UpdateText(string text)
		{
			SetValue(this.textName, text);
		}

		void OnDestory()
		{
			if (isUpdateChanged)
			{
				uiInputField.onValueChange.RemoveListener(UpdateText);
			} 
			else
			{
				uiInputField.onEndEdit.RemoveListener(UpdateText);
			}
		}
	}
}