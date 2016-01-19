using System;
using System.Collections;
using System.Collections.Generic;

namespace MenkoiMonster.Battle
{
	/// <summary>
	/// 状態変化の管理・操作クラス
	/// </summary>
	public class StatusEffectController
	{
		List<IStatusEffect> runtimeStatusEffects;

		public StatusEffectController()
		{
			runtimeStatusEffects = new List<IStatusEffect>();
		}

		/// <summary>
		/// 状態異常の追加
		/// </summary>
		/// <param name="statusEffect"></param>
		public void AddStatusEffect(IStatusEffect statusEffect)
		{
			runtimeStatusEffects.Add(statusEffect);
			statusEffect.OnStartStatusEffect();
		}

		/// <summary>
		/// ターン経過
		/// </summary>
		public void ElapsedTurn()
		{
			runtimeStatusEffects.ForEach(runtime => runtime.OnElapseTurn());
			runtimeStatusEffects.RemoveAll(CheckEnd);
		}

		bool CheckEnd(IStatusEffect runtime)
		{
			if (runtime.IsEndEffect())
			{
				runtime.OnEndStatusEffect();
				return true;
			}
			return false;
		}
	}
}