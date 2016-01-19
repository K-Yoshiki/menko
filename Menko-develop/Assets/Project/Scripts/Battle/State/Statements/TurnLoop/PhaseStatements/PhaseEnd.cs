using UnityEngine;
using System.Collections;
using AppUtils;

namespace MenkoiMonster.Battle.State.Turn
{
	public class PhaseEnd : PhaseStateBase
	{
		public PhaseEnd(BattleManager manager, PhaseShare share, bool isPlayer)
			: base(manager, share, isPlayer)
		{
		}

		public override PhaseStateName GetKey()
		{
			return PhaseStateName.End;
		}
	}
}