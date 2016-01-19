using UnityEngine;
using System;
using System.Collections;

namespace MenkoiMonster.Battle
{
	public class BattleEndUI : MonoBehaviour
	{
		[SerializeField] AttachedTween start;
		Action callback;

		public void Play(Action endCallback)
		{
			start.Execute();
			gameObject.SetActive(true);
			this.callback = endCallback;
		}

		public void OnEnd()
		{
			if (callback != null)
				callback();
			callback = null;
		}
	}
}