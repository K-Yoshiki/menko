using UnityEngine;
using System.Collections;

namespace AppUtils.UserControls
{
	public class EmulateInfo : IGestureInfo
	{
		int fingerID;

		public TouchState state;
		public Vector2 pos;
		public Vector2 deltaPos;
		public float stateTime;
		public Vector2 totalVec;
		public float totalTime;

		/// <summary>
		/// 指に付随されたID
		/// </summary>
		public int FingerID { get { return this.fingerID; } }

		/// <summary>
		/// このタッチの状態
		/// </summary>
		public TouchState State { get { return this.state; } }

		/// <summary>
		/// 現在のタッチ位置
		/// </summary>
		public Vector2 Pos { get { return this.pos; } }

		/// <summary>
		/// 前フレーム位置からの差分ベクトル
		/// </summary>
		public Vector2 DeltaPos { get { return this.deltaPos; } }

		/// <summary>
		/// 状態ごとの経過秒数
		/// </summary>
		public float StateTime { get { return this.stateTime; } }

		/// <summary>
		/// 開始位置から現在位置へのベクトル
		/// </summary>
		public Vector2 TotalVec { get { return this.totalVec; } }

		/// <summary>
		/// タッチ開始から終了までの合計時間
		/// </summary>
		/// <value>The total time.</value>
		public float TotalTime { get { return this.totalTime; } }

		public EmulateInfo()
		{
			this.fingerID = 0;
		}
	}
}