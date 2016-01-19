using System;
using UnityEngine;

namespace AppUtils.MVVM
{
	[AddComponentMenu("MVVM/Components/View Animator")]
	[RequireComponent(typeof(Animator))]
	public class ViewAnimator : View
	{
		[Header("Property Names")]
		[SerializeField] string enabledName;
		[SerializeField] string animatorName;
		[SerializeField] string playStateName;
		Animator animator;

		protected override void Init()
		{
			animator = GetComponent<Animator>();
			AddUpdater(enabledName, UpdateEnabled);
			AddUpdater(animatorName, UpdateAnimator);
			AddUpdater(playStateName, UpdatePlayState);
		}

		void UpdateEnabled(object value)
		{
			var active = (bool)value;
			if (!active)
			{
				animator.Stop();
			}
			animator.enabled = active;
		}

		void UpdateAnimator(object value)
		{
			animator.runtimeAnimatorController = (RuntimeAnimatorController)value;
		}

		void UpdatePlayState(object value)
		{
			animator.Play((string)value);
		}
	}
}