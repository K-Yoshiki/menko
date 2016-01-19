using UnityEngine;
using System.Collections.Generic;
using AppUtils;

namespace MenkoiMonster.Battle.State
{
	/// <summary>
	/// ゲーム開始
	/// </summary>
	public class StartGame : BattleStateBase
	{
		bool isPlayerFirst;
		TurnLoop turnLoop;

		public StartGame(BattleManager manager, bool isPlayerFirst) : base(manager)
		{
			this.isPlayerFirst = isPlayerFirst;
		}

		public override void Init(StateMediator<BattleStateName> mediator)
		{
			Debug.Log("ゲームスタート");

			// ターンのループステートを事前生成しておく
			turnLoop = new TurnLoop(manager, isPlayerFirst);

			// 使用メンコの初期生成
			PreInstantiate(manager.Data.AllBattleData);
		}

		public override void Update(StateMediator<BattleStateName> mediator)
		{
			if (IsEnd())
			{
				mediator.SetState(turnLoop.CurrentTurnState());
			}
		}

		public override void Exit(StateMediator<BattleStateName> mediator)
		{

		}

		bool IsEnd()
		{
			return true;
		}

		public override BattleStateName GetKey()
		{
			return BattleStateName.StartGame;
		}

		void PreInstantiate(MenkoBattleData[] allData)
		{
			var menkolist = manager.MenkoList;
			allData.Foreach(data => {
				var circle = Random.insideUnitCircle * 0.5f;
				var pos = new Vector3(circle.x, BattleConst.Map.FloorHeight, circle.y);
				var menko = Menko.CreateInstance(data, manager.SkillController, pos);
				menkolist.Add(menko);
			});
		}
	}
}