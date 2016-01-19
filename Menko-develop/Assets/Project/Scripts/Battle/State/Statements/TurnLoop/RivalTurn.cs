using UnityEngine;
using AppUtils;

namespace MenkoiMonster.Battle.State
{
	/// <summary>
	/// 対戦相手の攻撃ターン
	/// </summary>
	public class RivalTurn : BattleStateBase
	{
		PhaseStateMachine phaseSM;
		TurnLoop turnLoop;

		// debug
		float timer;

		public RivalTurn(BattleManager manager, TurnLoop turnLoop) : base(manager)
		{
			this.phaseSM = new PhaseStateMachine(manager, false);
			this.turnLoop = turnLoop;
			this.timer = 0;
		}

		#region IState implementation

		public override void Init(StateMediator<BattleStateName> mediator)
		{
			Debug.Log("対戦相手のターン");
			timer = 0;
			manager.ViewModels.GuideVM.GuideText = @"<color=""blue"">相手</color>のターンです";
//			this.phaseSM.SetState(PhaseStateName.SelectChip);
		}

		public override void Update(StateMediator<BattleStateName> mediator)
		{
//			this.phaseSM.UpdateState();
			timer += Time.deltaTime;
			if (IsEnd())
			{
				mediator.SetState(turnLoop.NextTurnState());
			}
		}

		public override void Exit(StateMediator<BattleStateName> mediator)
		{
			
		}

		bool IsEnd()
		{
			return timer > 2.0f;
		}

		public override BattleStateName GetKey()
		{
			return BattleStateName.RivalTurn;
		}

		#endregion
	}
}