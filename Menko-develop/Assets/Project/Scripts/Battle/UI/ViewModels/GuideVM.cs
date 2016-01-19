using UnityEngine;
using System.Collections;
using AppUtils.MVVM;

namespace MenkoiMonster.Battle
{
	public class GuideVM : ViewModel
	{
		string guideText;
		bool selecterEnabled;

		public GuideVM()
		{
			Bind("GuideText", () => guideText, null);
			Bind("SelecterEnabled", () => selecterEnabled, null);
		}

		public string GuideText
		{
			get { return guideText; }
			set
			{
				guideText = value;
				RaiseUpdate("GuideText");
			}
		}

		public bool SelecterEnabled
		{
			get { return selecterEnabled; }
			set
			{
				selecterEnabled = value;
				RaiseUpdate("SelecterEnabled");
			}
		}
	}
}