using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

namespace MenkoiMonster.Battle
{
	public class DamageCounter : MonoBehaviour
	{
		[SerializeField] Text uiDamage;
		[SerializeField] float duration;
		[SerializeField] Vector2 startScale;
		[SerializeField] Vector2 endScale;
		Action<DamageCounter> callback;
		RectTransform selfTf;
		float timer;

		public void ShowDamage(int damage, Vector2 screenPos,  Action<DamageCounter> callback)
		{
			this.gameObject.SetActive(true);
			this.selfTf.SetPosXY(screenPos.x, screenPos.y);
			this.callback = callback;
			uiDamage.text = damage.ToString();
			timer = 0f;
		}

		void Awake()
		{
			selfTf = GetComponent<RectTransform>();
		}

		void Update()
		{
			float x = Easing.ElasticOut(startScale.x, endScale.x, duration, timer);
			float y = Easing.ElasticOut(startScale.y, endScale.y, duration, timer);
			selfTf.localScale = new Vector3(x, y, 0f);

			timer += Time.deltaTime;
			if (duration < timer)
			{
				callback(this);
			}
		}
	}
}