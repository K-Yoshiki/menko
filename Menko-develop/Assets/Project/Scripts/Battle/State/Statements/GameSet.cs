using UnityEngine;
using AppUtils;

namespace MenkoiMonster.Battle.State
{
	/// <summary>
	/// 対戦終了
	/// </summary>
	public class GameSet : BattleStateBase
	{
		StateMediator<BattleStateName> mediator;

		public GameSet(BattleManager manager) : base(manager)
		{
		}

		#region IState implementation

		public override void Init(StateMediator<BattleStateName> mediator)
		{
			Debug.Log("対戦終了");
			this.mediator = mediator;

			//TODO: ネットワーク対戦の場合はこの時点でルーム解散してログアウトする

			// 対戦終了演出の開始
			manager.UI.PlayEnd(OnEnd);
		}

		void OnEnd()
		{
			mediator.SetState(new Result(manager));
		}

		public override BattleStateName GetKey()
		{
			return BattleStateName.GameSet;
		}

		#endregion
	}
}