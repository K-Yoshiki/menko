using System;
using UnityEngine;

namespace MenkoiMonster.Battle
{
	public class TestSkill : SkillBase
	{
		/// <summary>
		/// スキルの実行
		/// </summary>
		protected override void Execute()
		{

			// 扇形、画面上に向かって、メンコ2個分、130度角範囲でメンコの取得
			Menko[] hitList = AttackRange.Sector(this.transform, 2f, 130f);


			hitList.Foreach(hit => {
				// プレイヤーのメンコも含まれているためそれ以外にダメージ
				if (hit.IsPlayer() == false)
				{
					hit.SkillDamage((uint)battleData.SkillData.Values[0], battleData.SkillData.Element);
				}
			});

			End();
		}
	}
}