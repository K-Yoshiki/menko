using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace MenkoiMonster.Home
{
	public class FadeIn : MonoBehaviour
	{
		private Image screen;
		private float alpha;
		private bool isFade;

		public void HideScreen()
		{
			isFade = true;
		}

		void Awake()
		{
			screen = this.GetComponent<Image>();
			alpha = 1.0f;
		}

		void Update()
		{
			if (isFade == false)
				return;

			alpha -= 0.3f * Time.deltaTime;
			screen.color = new Color(0, 0, 0, alpha);
			if (alpha <= 0)
			{
				Destroy(this.gameObject);
			}
		}
	}
}