using UnityEngine;
using AppUtils;

namespace MenkoiMonster.Battle.State
{
	/// <summary>
	/// ゲームのリザルト表示
	/// </summary>
	public class Result : BattleStateBase
	{
		public Result(BattleManager manager) : base(manager)
		{
		}

		#region IState implementation

		public override void Init(StateMediator<BattleStateName> mediator)
		{
			Debug.Log("結果表示");

			//TODO: 勝敗結果データを入れていく
			//TODO: 結果表示のUI展開

			OnPressed();
		}

		public override void Update(StateMediator<BattleStateName> mediator)
		{

		}

		public override void Exit(StateMediator<BattleStateName> mediator)
		{

		}

		public override BattleStateName GetKey()
		{
			return BattleStateName.Result;
		}

		void OnPressed()
		{
			manager.EndBattle();
		}

		#endregion
	}
}