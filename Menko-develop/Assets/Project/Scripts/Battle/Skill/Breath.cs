using UnityEngine;
using System;


namespace MenkoiMonster.Battle
{
	/// <summary>
	/// 	扇型、円形範囲攻撃スキル
	/// 	扇型攻撃(狐1/狐2/ドワーフ/蛇1/カモメ/蛙1)
	/// 	円形攻撃(狐3/馬1)
	/// </summary>
	public class Breath : SkillBase
	{
		/// <summary>
		///  扇範囲での攻撃 
		/// </summary>
		void BreathAttack()
		{

			// 扇形、画面上に向かって、メンコ何個分、角度範囲でメンコの取得
			Menko[] hitList = AttackRange.Sector(this.transform, (uint)battleData.SkillData.Values[1], (uint)battleData.SkillData.Values[2]);	 
			//Values[1] = 攻撃の長さ（メンコ何個分か）
			//Values[2] = 角度
			
			hitList.Foreach(hit => {
				// プレイヤーのメンコも含まれているためそれ以外にダメージ
				if (hit.IsPlayer() == false)
				{
					hit.SkillDamage((uint)battleData.SkillData.Values[0], battleData.SkillData.Element);
				}
			});

		}

		protected override void Execute()
		{
			BreathAttack();

			End();
		}

	}
}