namespace MenkoiMonster.Battle
{
	/// <summary>
	/// ステータス変化、特殊変化などの状態変化・異常の実行のためのインタフェース
	/// </summary>
	public interface IStatusEffect
	{
		/// <summary>
		/// 状態変化の開始時
		/// </summary>
		void OnStartStatusEffect();

		/// <summary>
		/// 状態変化の終了時
		/// </summary>
		void OnEndStatusEffect();

		/// <summary>
		/// ターン経過時
		/// </summary>
		void OnElapseTurn();

		/// <summary>
		/// 効果終了したかどうか
		/// </summary>
		/// <returns></returns>
		bool IsEndEffect();
	}
}