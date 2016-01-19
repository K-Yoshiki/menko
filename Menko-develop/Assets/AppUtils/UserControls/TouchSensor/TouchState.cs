using UnityEngine;
using System.Collections;

namespace AppUtils.UserControls
{
	/// <summary>
	/// タッチの状態
	/// </summary>
	public enum TouchState
	{
		/// <summary>
		/// 触れた瞬間
		/// </summary>
		Enter,

		/// <summary>
		/// 触れた状態で移動している
		/// </summary>
		Move,

		/// <summary>
		/// 触れた状態で静止している
		/// </summary>
		Stay,

		/// <summary>
		/// 放した瞬間
		/// </summary>
		Exit,

		/// <summary>
		/// 触れている状態がアプリのスリープやセンサーエラーによってキャンセルされた
		/// </summary>
		Cancel,

		/// <summary>
		/// タッチの動作が終了している
		/// </summary>
		Closed
	}

	public static class TouchStateUtil
	{
		public static TouchState PhaseToState(TouchPhase phase)
		{
			switch (phase)
			{
				case TouchPhase.Began:
					return TouchState.Enter;
				case TouchPhase.Moved:
					return TouchState.Move;
				case TouchPhase.Stationary:
					return TouchState.Stay;
				case TouchPhase.Ended:
					return TouchState.Exit;
				case TouchPhase.Canceled:
					return TouchState.Cancel;
				default:
					return TouchState.Closed;
			}
		}
	}
}