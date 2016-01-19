using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AppUtils.UserControls
{
	/// <summary>
	/// タッチ情報
	/// </summary>
	public class TouchInfo : IGestureInfo
	{
		int fingerID;
		TouchState state;
		Vector2 pos;
		Vector2 deltaPos;
		float stateTime;
		Vector2 totalVec;
		float totalTime;

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

		public TouchInfo()
		{
			fingerID = 0;
		}

		public TouchInfo(Touch touch)
		{
			this.Init(touch);
		}

		public void Init(Touch touch)
		{
			this.fingerID = touch.fingerId;
			this.stateTime = touch.deltaTime;
			this.totalTime = touch.deltaTime;
			this.pos = touch.position;
			this.deltaPos = touch.deltaPosition;
			this.totalVec = touch.deltaPosition;
			this.state = TouchStateUtil.PhaseToState(touch.phase);
		}

		public void Update(Touch touch)
		{
			this.stateTime += touch.deltaTime;
			this.pos = touch.position;
			this.deltaPos = touch.deltaPosition;
			this.totalVec += this.DeltaPos;
			this.totalTime += touch.deltaTime;

			TouchState state = TouchStateUtil.PhaseToState(touch.phase);
			if (this.state != state)
			{
				this.state = state;
				this.stateTime = touch.deltaTime;
			}
		}

		public void Close()
		{
			this.state = TouchState.Closed;
		}
	}
}