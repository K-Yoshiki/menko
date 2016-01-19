using UnityEngine;
using System;

namespace AppUtils.MVVM
{
	/// <summary>
	/// ボタン要素のビュークラス
	/// </summary>
	[AddComponentMenu("MVVM/Components/View Animation")]
	[RequireComponent(typeof(Animation))]
	public class ViewAnimation : View
	{
		[Header("Property Names")]
		[SerializeField] string enabledName;
		[SerializeField] string clipName;
		Animation anim;

		protected override void Init()
		{
			anim = GetComponent<Animation>();
			AddUpdater(enabledName, UpdateEnabled);
			AddUpdater(clipName, UpdateClip);
		}

		void UpdateEnabled(object value)
		{
			anim.enabled = (bool)value;
			if (anim.enabled)
			{
				anim.Play();
			}
			else
			{ 
				anim.Stop();
			}
		}

		void UpdateClip(object value)
		{
			anim.clip = (AnimationClip)value;
		}
	}
}