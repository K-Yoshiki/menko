using UnityEngine;
using AppUtils;

namespace MenkoiMonster.Battle.State
{
	/// <summary>
	/// ターンの終了
	/// </summary>
	public class EndTurn : BattleStateBase
	{
		TurnLoop turnLoop;

		public EndTurn(BattleManager manager, TurnLoop turnLoop) : base(manager)
		{
			this.turnLoop = turnLoop;
		}

		#region IState implementation

		public override void Init(StateMediator<BattleStateName> mediator)
		{
			Debug.Log("お互いの攻撃フェーズが全て終了");
			mediator.SetState(turnLoop.NextTurnState());
		}

		public override BattleStateName GetKey()
		{
			return BattleStateName.EndTurn;
		}

		#endregion
	}
}