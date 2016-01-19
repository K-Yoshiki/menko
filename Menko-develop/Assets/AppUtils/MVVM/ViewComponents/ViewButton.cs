using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

namespace AppUtils.MVVM
{
	/// <summary>
	/// ボタン要素のビュークラス
	/// </summary>
	[AddComponentMenu("MVVM/Components/View Button")]
	[RequireComponent(typeof(Button))]
	public class ViewButton : View
	{
		[Header("Property Names")]
		[SerializeField] string enabledName;
		[SerializeField] string interactableName;
		[SerializeField] string pressEventName;
		[SerializeField] string longPressEventName;
		PressEventer pressEventer;
		Button button;

		protected override void Init()
		{
			button = GetComponent<Button>();
			AddUpdater(enabledName, UpdateEnabled);
			AddUpdater(pressEventName, UpdatePressAction);
			AddUpdater(interactableName, UpdateInteractable);
			AddUpdater(longPressEventName, UpdateLongPressAction);

			var trigger = GetComponent<EventTrigger>();
			trigger = trigger ?? gameObject.AddComponent<EventTrigger>();
			pressEventer = new PressEventer(trigger);
        }

		void Update()
		{
			pressEventer.UpdateProcess();
		}

		void UpdateEnabled(object value)
		{
			button.enabled = (bool)value;
		}

		void UpdateInteractable(object value)
		{
			button.interactable = (bool)value;
			pressEventer.intaractable = (bool)value;
		}

		void UpdatePressAction(object value)
		{
			pressEventer.OnPress = (Action)value;
		}

		void UpdateLongPressAction(object value)
		{
			pressEventer.OnLongPress = (Action)value;
		}
	}

	public class PressEventer
	{
		EventTrigger trigger;
		Action onPress;
		Action onLongPress;
		float timer;
		bool isDown;
		const float LongPressThrethold = 1.0f;

		public PressEventer(EventTrigger trigger)
		{
			this.trigger = trigger;

			var onDown = new EventTrigger.Entry();
			onDown.eventID = EventTriggerType.PointerDown;
			onDown.callback.AddListener(OnDown);
			trigger.triggers.Add(onDown);

			var onCancel = new EventTrigger.Entry();
			onCancel.eventID = EventTriggerType.Cancel;
			onCancel.callback.AddListener(ResetTimer);
			trigger.triggers.Add(onCancel);

			var onExit = new EventTrigger.Entry();
			onExit.eventID = EventTriggerType.PointerExit;
			onExit.callback.AddListener(ResetTimer);
			trigger.triggers.Add(onExit);

			var onUp = new EventTrigger.Entry();
			onUp.eventID = EventTriggerType.PointerUp;
			onUp.callback.AddListener(OnUp);
			trigger.triggers.Add(onUp);
		}

		public bool intaractable
		{
			get { return trigger.enabled; }
			set { trigger.enabled = value; }
		}

		public Action OnPress
		{
			set { onPress = value; }
		}

		public Action OnLongPress
		{
			set { onLongPress = value; }
		}

		public void UpdateProcess()
		{
			if (isDown == false)
				return;

			timer += Time.deltaTime;
			if (timer > LongPressThrethold)
			{
				InvokeOnLongPress();
			}
		}

		void OnDown(BaseEventData e)
		{
			isDown = true;
		}

		void OnUp(BaseEventData e)
		{
			if (isDown)
			{
				InvokeOnPress();
			}
		}

		void ResetTimer(BaseEventData e)
		{
			isDown = false;
			timer = 0f;
		}

		void InvokeOnPress()
		{
			if (onPress != null)
				onPress();
			ResetTimer(null);
		}

		void InvokeOnLongPress()
		{
			Debug.Log("On Long Pressed!");
			if (onLongPress == null)
			{
				InvokeOnPress();
				return;
			}
			onLongPress();
			ResetTimer(null);
		}
	}
}