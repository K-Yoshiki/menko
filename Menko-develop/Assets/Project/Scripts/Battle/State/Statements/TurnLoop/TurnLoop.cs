using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AppUtils;

namespace MenkoiMonster.Battle.State
{
	public class TurnLoop
	{
		BattleManager manager;
		IState<BattleStateName>[] turnList;
		int index;

		public TurnLoop(BattleManager manager, bool isPlayerFirst)
		{
			this.manager = manager;
			index = 0;
			this.DefSet(manager, isPlayerFirst);
		}

		public IState<BattleStateName> CurrentTurnState()
		{
			return turnList[index];
		}

		public IState<BattleStateName> NextTurnState()
		{
			if (IsFinished())
			{
				// どちらかの場の代表格メンコ数が0になったら試合終了へ
				return new GameSet(manager);
			}
			index = Repeat(index + 1, 3);
			var result = turnList[index];
			return result;
		}

		bool IsFinished()
		{
			return manager.MenkoList.IsFinished();
		}

		int Repeat(int i, int length)
		{
			return (i < length) ? i : (i % length);
		}

		void DefSet(BattleManager manager, bool isPlayerFirst)
		{
			turnList = new IState<BattleStateName>[3];
			var playerTurn = new PlayerTurn(manager, this);
			var rivalTurn = new RivalTurn(manager, this);
			var endTurn = new EndTurn(manager, this);

			if (isPlayerFirst)
			{
				turnList[0] = playerTurn;
				turnList[1] = rivalTurn;
				turnList[2] = endTurn;
			}
			else
			{
				turnList[0] = rivalTurn;
				turnList[1] = playerTurn;
				turnList[2] = endTurn;
			}
		}
	}
}