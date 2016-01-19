using UnityEngine;
using System.Collections;
using AppUtils;

namespace MenkoiMonster.Battle.State
{
	public abstract class BattleStateBase : IState<BattleStateName>
	{
		protected BattleManager manager;

		public BattleStateBase(BattleManager manager)
		{
			this.manager = manager;
		}

		#region IState implementation

		public virtual void Init(StateMediator<BattleStateName> mediator)
		{
		}

		public virtual void Update(StateMediator<BattleStateName> mediator)
		{
		}

		public virtual void Exit(StateMediator<BattleStateName> mediator)
		{
		}

		public abstract BattleStateName GetKey();

		#endregion
	}
}