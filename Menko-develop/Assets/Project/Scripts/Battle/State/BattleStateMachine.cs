using UnityEngine;
using AppUtils;

namespace MenkoiMonster.Battle.State
{
	public class BattleStateMachine
	{
		StateMachine<BattleStateName> stateMachine;
		BattleManager battleManager;

		public BattleStateMachine(BattleManager manager)
		{
			this.battleManager = manager;
			stateMachine = new StateMachine<BattleStateName>();
		}

		public void Init()
		{
			stateMachine.SetState(new AwakeGame(battleManager));
		}

		public void Update()
		{
			stateMachine.UpdateState();
		}
	}
}