using UnityEngine;
using System.Collections;

namespace MenkoiMonster.Battle
{
	public class QuakeEffect : Effect
	{
		[SerializeField] float maxSize;
		[SerializeField] SpriteRenderer spriteRender;

		void Awake()
		{
			if (selfTf == null)
				selfTf = this.transform;

			StartCoroutine(Execute());
		}

		IEnumerator Execute()
		{
			Vector3 lastSize = new Vector3(maxSize, maxSize, 1.0f);
			Color tempColor = spriteRender.color;
			float colorAmount = tempColor.a / duration;
			float speed = 1 / duration;

			while (duration > 0f)
			{
				selfTf.localScale = Vector3.Slerp(selfTf.localScale, lastSize, Time.deltaTime * speed);
				tempColor.a -= Time.deltaTime * colorAmount;
				spriteRender.color = tempColor;
				duration -= Time.deltaTime;
				yield return null;
			}
		}
	}
}