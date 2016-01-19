using UnityEngine;
using System.Collections.Generic;
using AppUtils.MVVM;
using AppUtils.Assets;

namespace MenkoiMonster.Battle
{
	public class BattleViewModels
	{
		UnitListVM unitListVM;
		BattleVM battleVM;
		GuideVM guideVM;

		public UnitListVM UnitListVM
		{
			get { return this.unitListVM; }
		}

		public BattleVM BattleVM
		{
			get { return this.battleVM; }
		}

		public GuideVM GuideVM
		{
			get { return this.guideVM; }
		}

		public void BindVM(BattleData data)
		{
			ViewRoot root = Object.FindObjectOfType<ViewRoot>();
			List<UnitVM> playerUnits = CreateUnitVMList(data.PlayerUnit.GetData());
			List<UnitVM> rivalUnits = CreateUnitVMList(data.RivalUnit.GetData());
			this.unitListVM = new UnitListVM(playerUnits, rivalUnits);
			this.battleVM = new BattleVM(root);
			this.guideVM = new GuideVM() { GuideText = "" };
			root.Bind(this.unitListVM);
			root.Bind(this.guideVM);
			root.SetContext();
		}

		List<UnitVM> CreateUnitVMList(MenkoBattleData[] unitList)
		{
			List<UnitVM> unitVMList = new List<UnitVM>();
			for (int i = 0; i < unitList.Length; ++i)
			{
				var unit = unitList[i];
				string path = AssetPath.GetMonsterFaceTexPath(unit.BaseData.ID);
				var assetData = AssetManager.Load<Sprite>(path);
				var face = assetData.Asset as Sprite;
				var vm = new UnitVM(face, unit.IsRepresent, unit.SkillData.NeedTurn);
                unitVMList.Add(vm);
				unit.UnitViewModel = vm;
			}
			return unitVMList;
		}
	}
}