namespace AppUtils
{
	/// <summary>
	/// ステートのベース
	/// </summary>
	public interface IState<StateKey>
	{
		/// <summary>
		/// ステートの開始
		/// </summary>
		void Init(StateMediator<StateKey> mediator);

		/// <summary>
		/// ステートの更新
		/// </summary>
		void Update(StateMediator<StateKey> mediator);

		/// <summary>
		/// ステート終了時処理
		/// </summary>
		void Exit(StateMediator<StateKey> mediator);

		/// <summary>
		/// このステートのキー
		/// </summary>
		StateKey GetKey();
	}
}