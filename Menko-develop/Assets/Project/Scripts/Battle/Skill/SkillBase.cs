using UnityEngine;
using System;

namespace MenkoiMonster.Battle
{
	/// <summary>
	/// スキル実行ベースクラス
	/// </summary>
	public abstract class SkillBase : MonoBehaviour
	{
		[SerializeField]
		protected ushort identifier;
		protected MenkoList menkoList;
		protected MenkoBattleData battleData;
        Transform selfTf;
		Menko invoker;
		Action callback;

		public new Transform transform
		{
			get { return selfTf != null ? selfTf : selfTf = base.transform; }
		}

		/// <summary>
		/// スキルの実行
		/// </summary>
		/// <param name="skillData"></param>
		public void Run(MenkoList menkoList, MenkoBattleData battleData, Menko invoker, Action callback)
		{
			Debug.Log("Run Start");
			if (battleData == null)
				return;

			this.menkoList = menkoList;
			this.battleData = battleData;
			this.invoker = invoker;
			this.callback = callback;
			Execute();
			Debug.Log("Executed - skillbase");
		}

		/// <summary>
		/// スキルの実行処理
		/// </summary>
		/// <param name="skillData"></param>
		protected abstract void Execute();

		/// <summary>
		/// スキルの実行終了
		/// </summary>
		protected void End()
		{
			battleData.Status.ResetSkillEnergy();
			if (callback != null)
			{
				callback();
				callback = null;
			}
			Destroy(this.gameObject);
		}
	}
}
