using UnityEngine;
using System;

namespace MenkoiMonster.Battle
{
	/// <summary>
	/// ひっくり返すスキル
	/// </summary>
	public class Opposite : SkillBase 
	{

		void OppositeAttack()
		{
			// 扇形、画面上に向かって、メンコ何個分、角度範囲でメンコの取得
			Menko[] hitList = AttackRange.Sector(this.transform, (uint)battleData.SkillData.Values[1], (uint)battleData.SkillData.Values[2]);

			hitList.Foreach(hit => {
				// プレイヤーのメンコも含まれているためそれ以外にダメージ
				if (hit.IsPlayer() == false)
				{

					hit.SkillDamage((uint)battleData.SkillData.Values[0], battleData.SkillData.Element); 

					if(UnityEngine.Random.Range(0.0f,1.0f) >= 0.5f)
					{
						hit.transform.Rotate(180,0,0);
					}

				}
			});
		}

		protected override void Execute()
		{
			OppositeAttack();

			End();
		}
	}
}