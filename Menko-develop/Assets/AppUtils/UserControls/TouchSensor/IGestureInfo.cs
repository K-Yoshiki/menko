using UnityEngine;
using System.Collections;

namespace AppUtils.UserControls
{
	public interface IGestureInfo
	{
		/// <summary>
		/// ID
		/// </summary>
		int FingerID { get; }

		/// <summary>
		/// このタッチの状態
		/// </summary>
		TouchState State { get; }

		/// <summary>
		/// 現在のタッチ位置
		/// </summary>
		Vector2 Pos { get; }

		/// <summary>
		/// 前フレーム位置からの差分ベクトル
		/// </summary>
		Vector2 DeltaPos { get; }

		/// <summary>
		/// 状態ごとの経過秒数
		/// </summary>
		float StateTime { get; }

		/// <summary>
		/// 開始位置から現在位置へのベクトル
		/// </summary>
		Vector2 TotalVec { get; }

		/// <summary>
		/// タッチ開始から終了までの合計時間
		/// </summary>
		/// <value>The total time.</value>
		float TotalTime { get; }

	}
}