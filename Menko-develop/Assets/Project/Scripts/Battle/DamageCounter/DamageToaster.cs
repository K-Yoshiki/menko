using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AppUtils;

namespace MenkoiMonster.Battle
{
	public class DamageToaster : UnitySingleton<DamageToaster>
	{
		[SerializeField] DamageCounter template;
		Queue<DamageCounter> counterQueue; // ダメージ表示のプール用Stack
		[SerializeField] Camera uiCamera;

		public void ShowDamage(int damage, Vector3 position)
		{
			DamageCounter counter;
			Vector3 showUpside = new Vector3(0f, 0f, 0.25f);
			Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(GetCamera(), position + showUpside);

			if (counterQueue.Count <= 0)
			{
				counter = Instantiate(template);
				counter.transform.SetParent(this.transform);
			}
			else
			{
				counter = counterQueue.Dequeue();
			}

			counter.ShowDamage(damage, screenPos, RemoveCounter);
		}

		protected override void Initialize()
		{
			counterQueue = new Queue<DamageCounter>();
			uiCamera = Camera.main;
		}

		void RemoveCounter(DamageCounter counter)
		{
			counter.gameObject.SetActive(false);
			counterQueue.Enqueue(counter);
		}

		Camera GetCamera()
		{
			if (uiCamera == null)
			{
				uiCamera = Camera.main;
			}
			return uiCamera;
		}
	}
}