namespace AppUtils
{
	/// <summary>
	/// ステート機能クラス
	/// </summary>
	public class StateMachine<StateKey>
	{
		StateMediator<StateKey> mediator;
		IState<StateKey> current;

		public StateMachine()
		{
			mediator = new StateMediator<StateKey>(this);
		}

		/// <summary>
		/// ステートの切り替え
		/// </summary>
		/// <param name="state">State.</param>
		public void SetState(IState<StateKey> state)
		{
			if (current != null)
			{
				current.Exit(mediator);
			}
			current = state;
			current.Init(mediator);
		}

		/// <summary>
		/// ステートの更新
		/// </summary>
		public void UpdateState()
		{
			if (current == null)
			{
				return;
			}
			current.Update(mediator);
		}

		/// <summary>
		/// 現在のステートのキー
		/// </summary>
		public StateKey CurrentKey
		{
			get
			{ 
				if (current == null)
					return default(StateKey);
				return current.GetKey();
			}
		}
	}
}