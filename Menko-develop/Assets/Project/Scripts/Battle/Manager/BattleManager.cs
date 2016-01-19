using UnityEngine;

namespace MenkoiMonster.Battle
{
	/// <summary>
	/// バトル全体の管理クラス
	/// </summary>
	public class BattleManager
	{
		BattleData data;
		BattleViewModels battleVM;
		BattleUI battleUI;
		MenkoList list;
		SkillController skillController;
		bool isBattleEnd;

		public BattleManager(BattleData data)
		{
			this.data = data;
			battleVM = new BattleViewModels();
			list = new MenkoList();
			skillController = new SkillController(this);
			isBattleEnd = false;
		}

		public void Init()
		{
			battleUI = Object.FindObjectOfType<BattleUI>();
			BindVM();
		}

		public BattleData Data
		{
			get { return data; }
		}

		public BattleViewModels ViewModels
		{
			get { return battleVM; }
		}

		public BattleUI UI
		{
			get { return battleUI; }
		}

		public MenkoList MenkoList
		{
			get { return list; }
		}

		public SkillController SkillController
		{
			get { return skillController; }
		}

		public bool IsPlayerHost
		{
			get { return data.IsPlayerHost; }
		}

		void BindVM()
		{
			battleVM.BindVM(data);
		}

		public void EndBattle()
		{
			this.isBattleEnd = true;
		}

		public bool IsBattleEnd()
		{
			return isBattleEnd;
		}
	}
}