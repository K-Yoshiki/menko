using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace MenkoiMonster.Battle
{
	/// <summary>
	/// スキル攻撃時の判定処理クラス
	/// </summary>
	public static class AttackRange
	{
		/// <summary>
		/// 指定された円形範囲内のメンコを返します.
		/// </summary>
		/// <param name="me">判定基準.</param>
		/// <param name="range">メンコ何個分の距離か.</param>
		public static Menko[] Circle(Transform me, float range)
		{
			return extract(Physics.OverlapSphere(
				me.position, BattleConst.Menko.Radius + (BattleConst.Menko.Radius * 2.0f * range)
			));
		}

		/// <summary>
		/// 指定された扇形範囲内のメンコを返します.
		/// </summary>
		/// <param name="me">判定基準.</param>
		/// <param name="range">メンコ何個分の判定距離か.</param>
		/// <param name="searchAngle">判定角度. 最大180度.</param>
		/// <param name="dir">判定方向. 0f = 画面上方向.</param>
		public static Menko[] Sector(Transform me, float range, float searchAngle, float dirAngle = 0f)
		{
			Menko[] hitArray = Circle(me, range);
			if (hitArray.Length <= 0)
				return hitArray;

			// 補正
			searchAngle = Mathf.Max(Mathf.Min(searchAngle, 180f), 0f) * 0.5f;
			dirAngle = Mathf.Max(Mathf.Min(dirAngle, 180f), -180f);

			// 角度判定
			return hitArray.Where(menko => {
				Vector3 dir = menko.transform.position - me.position;
				float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
				angle += dirAngle;

				return (angle <= searchAngle && -searchAngle <= angle);
			}).ToArray();
		}

		static Menko[] extract(Collider[] hits)
		{
			List<Menko> result = new List<Menko>();
			hits.Foreach(hit => {
				var menko = hit.transform.GetComponent<Menko>();
				if (menko != null)
				{
					result.Add(menko);
				}
			});
			return result.ToArray();
		}
	}
}