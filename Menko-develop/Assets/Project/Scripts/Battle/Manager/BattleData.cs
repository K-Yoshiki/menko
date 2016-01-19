using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AppUtils.Assets;

namespace MenkoiMonster.Battle
{
	public class BattleData
	{
		uint stageNumber;
		bool isPlayerHost;
		BattleUnit playerUnit;
		BattleUnit rivalUnit;

		public BattleData(uint stageNum, bool isPlayerHost, BattleUnit[] units)
		{
			this.stageNumber = stageNum;
			this.isPlayerHost = isPlayerHost;
			this.playerUnit = units[0];
			this.rivalUnit = units[1];
		}

		public uint StageNumber
		{
			get { return this.stageNumber; }
		}

		public bool IsPlayerHost
		{
			get { return isPlayerHost; }
		}

		public BattleUnit PlayerUnit
		{
			get { return this.playerUnit; }
		}

		public BattleUnit RivalUnit
		{
			get { return this.rivalUnit; }
		}

		public MenkoBattleData[] AllBattleData
		{
			get
			{
				return Enumerable.Concat(
					this.playerUnit.GetData(),
					this.rivalUnit.GetData()
				).ToArray();
			}
		}

		public AssetLoadPath[] GetPreLoadPaths()
		{
			var paths = new List<AssetLoadPath>();

			// stagePath
			paths.Add(new AssetLoadPath(AssetPath.GetStagePrefabPath(this.stageNumber)));
			// UnitPaths
			paths.AddRange(playerUnit.GetPreLoadPaths().Concat(rivalUnit.GetPreLoadPaths()));

			return paths.ToArray();
		}
	}
}