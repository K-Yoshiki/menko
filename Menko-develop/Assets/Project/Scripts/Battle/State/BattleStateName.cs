namespace MenkoiMonster.Battle.State
{
	public enum BattleStateName
	{
		/// <summary>
		/// ゲームの開始準備
		/// </summary>
		AwakeGame,

		/// <summary>
		/// 先攻決めフェーズ
		/// </summary>
		DecidePlayFirst,

		/// <summary>
		/// ゲームのスタート
		/// </summary>
		StartGame,

		/// <summary>
		/// プレイヤーのフェーズ
		/// </summary>
		PlayerTurn,

		/// <summary>
		/// 対戦相手のフェーズ
		/// </summary>
		RivalTurn,

		/// <summary>
		/// お互いの攻撃フェーズが全て終了
		/// </summary>
		EndTurn,

		/// <summary>
		/// 対戦終了
		/// </summary>
		GameSet,

		/// <summary>
		/// 結果表示
		/// </summary>
		Result,

		/// <summary>
		/// バトルの全過程終了
		/// </summary>
		AllEnd,
	}
}