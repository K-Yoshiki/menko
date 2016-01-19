using UnityEngine;
using System;
using AppUtils.MVVM;

namespace MenkoiMonster.Battle
{
	public class BattleVM
	{
		BackButtonVM backButton;
		SkillInfoVM skillInfo;
		SkillCutInVM skillCutIn;

		public BattleVM(ViewRoot root)
		{
			backButton = new BackButtonVM();
			skillInfo = new SkillInfoVM();
			skillCutIn = new SkillCutInVM();
			root.Bind(backButton);
			root.Bind(skillInfo);
			root.Bind(skillCutIn);
		}

		public BackButtonVM BackButtonVM
		{
			get { return backButton; }
		}

		public SkillInfoVM SkillInfoVM
		{
			get { return skillInfo; }
		}
		
		public SkillCutInVM SkillCutInVM
		{
			get { return skillCutIn; }
		}
	}

	public class BackButtonVM : ViewModel
	{
		bool enabled;
		Action action;

		public BackButtonVM()
		{
			Bind("Enabled", () => enabled, null);
			Bind("Action", () => action, null);
		}

		public bool Enabled
		{
			get { return enabled; }
			set
			{
				enabled = value;
				RaiseUpdate("Enabled");
			}
		}

		public Action Action
		{
			get { return action; }
			set
			{
				action = value;
				RaiseUpdate("Action");
			}
		}
	}

	public class SkillInfoVM : ViewModel
	{
		bool enabled;
		string skillName;
		string skillText;

		public SkillInfoVM()
		{
			Bind("Enabled", () => enabled, null);
			Bind("SkillName", () => skillName, null);
			Bind("SkillText", () => skillText, null);
		}

		public bool Enabled
		{
			get { return enabled; }
			set
			{
				enabled = value;
				RaiseUpdate("Enabled");
			}
		}

		public string SkillName
		{
			get { return skillName; }
			set
			{
				skillName = value;
				RaiseUpdate("SkillName");
			}
		}

		public string SkillText
		{
			get { return skillText; }
			set
			{
				skillText = value;
				RaiseUpdate("SkillText");
			}
		}
	}

	public class SkillCutInVM : ViewModel
	{
		bool enabled;
		string backBandStateName;
		Sprite monsterTex;
		Action endCallback;

		public SkillCutInVM()
		{
			backBandStateName = "Empty";
			Bind("Enabled", () => enabled, null);
			Bind("BackBandStateName", () => backBandStateName, null);
			Bind("MonsterTex", () => monsterTex, null);
			Bind("EndCallback", () => endCallback, null);
		}

		public bool Enabled
		{
			get { return enabled; }
			set
			{
				enabled = value;
				RaiseUpdate("Enabled");
			}
		}

		public string BackBandStateName
		{
			get { return backBandStateName; }
			set
			{
				backBandStateName = value;
				RaiseUpdate("BackBandStateName");
			}
		}

		public Sprite MonsterTex
		{
			get { return monsterTex; }
			set
			{
				monsterTex = value;
				RaiseUpdate("MonsterTex");
			}
		}

		public Action EndCallback
		{
			get { return endCallback; }
			set
			{
				endCallback = value;
				RaiseUpdate("EndCallback");
			}
		}
	}
}