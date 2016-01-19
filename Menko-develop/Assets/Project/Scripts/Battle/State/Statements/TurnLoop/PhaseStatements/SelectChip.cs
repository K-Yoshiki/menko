using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AppUtils;

namespace MenkoiMonster.Battle.State.Turn
{
	public class SelectChip : PhaseStateBase
	{
		bool isPressed;
		List<UnitVM> unitList;
		
		public SelectChip(BattleManager manager, PhaseShare share, bool isPlayer)
			: base(manager, share, isPlayer)
		{
		}

		public override void Init(StateMediator<PhaseStateName> mediator)
		{
			share.fallPointer.gameObject.SetActive(false);
			isPressed = false;
			share.selectIndex = -1;
			share.isUseSkill = false;
			if (isPlayer)
			{
				unitList = manager.ViewModels.UnitListVM.PlayerUnitList;
				for (int i = 0; i < unitList.Count; i++)
				{
					int index = i;
					unitList[i].Pressed = () => Pressed(index);
					unitList[i].LongPressed = () => LongPressed(index);
				}
				manager.ViewModels.GuideVM.SelecterEnabled = true;
			}
		}

		public override void Update(StateMediator<PhaseStateName> mediator)
		{
			if (IsEnd())
			{
				mediator.SetState(share.cache.GetState(PhaseStateName.SetFallPoint));
			}
		}

		public override void Exit(StateMediator<PhaseStateName> mediator)
		{
			manager.ViewModels.GuideVM.SelecterEnabled = false;
		}

		public override PhaseStateName GetKey()
		{
			return PhaseStateName.SelectChip;
		}

		void Pressed(int index)
		{
			share.isUseSkill = false;
			Select(index);
		}

		void LongPressed(int index)
		{
			var data = manager.Data.PlayerUnit.GetData(index);
			if (data.CanUseSkill)
			{
				share.isUseSkill = true;
			}
			Select(index);
		}

		void Select(int index)
		{
			if (share.selectIndex != -1)
			{
				unitList[share.selectIndex].IsPressable = true;
			}
			share.selectIndex = index;
			unitList[index].IsPressable = false;
			isPressed = true;
		}

		bool IsEnd()
		{
			return isPressed;
		}
	}
}