namespace AppUtils
{
	/// <summary>
	/// ステート間遷移用のクラス
	/// </summary>
	public class StateMediator<StateKey>
	{
		StateMachine<StateKey> stateMachine;

		public StateMediator(StateMachine<StateKey> stateMachine)
		{
			this.stateMachine = stateMachine;
		}

		/// <summary>
		/// ステートの切り替え
		/// </summary>
		public void SetState(IState<StateKey> state)
		{
			stateMachine.SetState(state);
		}

		/// <summary>
		/// 現在のステートのキー
		/// </summary>
		public StateKey CurrentState
		{
			get { return stateMachine.CurrentKey; }
		}
	}
}