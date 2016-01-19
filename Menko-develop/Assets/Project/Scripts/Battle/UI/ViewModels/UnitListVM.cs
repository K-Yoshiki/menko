using UnityEngine;
using System.Collections.Generic;
using AppUtils.MVVM;

namespace MenkoiMonster.Battle
{
	public class UnitListVM : ViewModel
	{
		List<UnitVM> playerUnits;
		List<UnitVM> rivalUnits;

		public UnitListVM(List<UnitVM> playerUnits, List<UnitVM> rivalUnits)
		{
			this.playerUnits = playerUnits;
			this.rivalUnits = rivalUnits;
			Binding();
		}

		void Binding()
		{
			Bind("PlayerUnitList", () => playerUnits, null);
			Bind("RivalUnitList", () => rivalUnits, null);
		}

		public List<UnitVM> PlayerUnitList
		{
			get { return playerUnits; }
			set
			{
				playerUnits = value;
				RaiseUpdate("PlayerUnitList");
			}
		}

		public List<UnitVM> RivalUnitList
		{
			get { return rivalUnits; }
			set
			{
				rivalUnits = value;
				RaiseUpdate("RivalUnitList");
			}
		}
	}
}