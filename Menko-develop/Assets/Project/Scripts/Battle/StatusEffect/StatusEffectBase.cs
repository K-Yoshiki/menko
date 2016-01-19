using System;
using UnityEngine;

namespace MenkoiMonster.Battle
{
	/// <summary>
	/// ステータス変化、特殊変化などの状態変化、異常などの実行のための基底クラス
	/// </summary>
	public abstract class StatusEffectBase : IStatusEffect
	{
		/// <summary>
		/// 残りターン数
		/// </summary>
		int remainingTurn;

		public StatusEffectBase(int remainingTurn)
		{
			this.remainingTurn = remainingTurn;
		}

		/// <summary>
		/// 効果が終了したかどうか
		/// </summary>
		/// <returns></returns>
		public bool IsEndEffect()
		{
			return remainingTurn <= 0;
		}

		/// <summary>
		/// ターン経過時処理
		/// </summary>
		public void OnElapseTurn()
		{
			remainingTurn--;
			ElapsedTurn();
		}
		
		/// <summary>
		/// ターン経過処理(派生先)
		/// </summary>
		protected virtual void ElapsedTurn()
		{
		}

		/// <summary>
		/// 状態変化の開始時
		/// </summary>
		public virtual void OnStartStatusEffect()
		{
		}

		/// <summary>
		/// 状態変化の終了時
		/// </summary>
		public virtual void OnEndStatusEffect()
		{	
		}
	}
}