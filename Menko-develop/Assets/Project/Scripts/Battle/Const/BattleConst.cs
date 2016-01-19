using UnityEngine;

namespace MenkoiMonster.Battle
{
	public static class BattleConst
	{
		/// <summary>
		/// メンコに関する共通情報
		/// </summary>
		public static class Menko
		{
			/// <summary>
			/// メンコのベースプレハブ情報
			/// </summary>
			public const string BaseMenkoPath = "Prefabs/Menko";

			/// <summary>
			/// 先攻後攻判定メンコのプレハブ情報
			/// </summary>
			public const string DecideMenkoPath = "Prefabs/DecideMenko";

			/// <summary>
			/// MenkoSizeでのスケール変動値
			/// </summary>
			public const float ScaleVariable = 0.2f;

			/// <summary>
			/// Menkoの半径サイズ
			/// </summary>
			public const float Radius = 0.2f;

			/// <summary>
			/// 初期状態のAngle
			/// </summary>
			const float DefaultAngle = -90f;

			/// <summary>
			/// 裏返されたとされる判定の角度
			/// </summary>
			const float DefeatThreshold = 120f;

			/// <summary>
			/// 裏返し判定の最小角度
			/// </summary>
			public static readonly float DefeatXMin = Mathf.Repeat(DefaultAngle + 180f - (DefeatThreshold * 0.5f), 360f);

			/// <summary>
			/// 裏返し判定の最大角度
			/// </summary>
			public static readonly float DefeatXMax = Mathf.Repeat(DefaultAngle + 180f + (DefeatThreshold * 0.5f), 360f);

			/// <summary>
			/// プレイヤーのメンコカラー
			/// </summary>
			public static readonly Color PlayerColor = new Color(0.9f, 0.05f, 0.05f);

			/// <summary>
			/// 相手のメンコカラー
			/// </summary>
			public static readonly Color RivalColor = new Color(0.05f, 0.05f, 0.9f);

			/// <summary>
			/// サイズ指定からスケール値へ変換する
			/// </summary>
			public static float SizeToScale(MenkoSize size)
			{
				switch (size)
				{
					case MenkoSize.Small:
						return 1f - ScaleVariable;
					case MenkoSize.Large:
						return 1f + ScaleVariable;
					default:
						return 1f;
				}
			}
		}

		public static class Map
		{
			public const float FloorHeight = 0.25f;
		}
	}
}