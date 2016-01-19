using UnityEngine;
using AppUtils;

namespace MenkoiMonster.Battle.State
{
	/// <summary>
	/// プレイヤーの攻撃ターン
	/// </summary>
	public class PlayerTurn : BattleStateBase
	{
		PhaseStateMachine phaseSM;
		TurnLoop turnLoop;

		public PlayerTurn(BattleManager manager, TurnLoop turnLoop) : base(manager)
		{
			phaseSM = new PhaseStateMachine(manager, true);
			this.turnLoop = turnLoop;
		}

		#region IState implementation

		public override void Init(StateMediator<BattleStateName> mediator)
		{
			Debug.Log("プレイヤーのターン");
			manager.ViewModels.GuideVM.GuideText = @"<color=""red"">あなた</color>のターンです";
			
			var unitList = manager.Data.PlayerUnit.GetData();
			var unitVMList = manager.ViewModels.UnitListVM.PlayerUnitList;
			for (int i = 0; i < unitList.Length; i++)
			{
				var unit = unitList[i];
				var unitVM = unitVMList[i];

				unit.Status.ElapseTurn();
				SetUpButton(unitVM);
			}

			phaseSM.SetState(PhaseStateName.SelectChip);
		}

		public override void Update(StateMediator<BattleStateName> mediator)
		{
			phaseSM.UpdateState();
			if (IsEnd())
			{
				mediator.SetState(turnLoop.NextTurnState());
			}
		}

		void SetUpButton(UnitVM unit)
		{
			unit.IsPressable = true;
		}

		bool IsEnd()
		{
			return phaseSM.CurrentKey == PhaseStateName.End;
		}

		public override BattleStateName GetKey()
		{
			return BattleStateName.PlayerTurn;
		}

		#endregion
	}
}