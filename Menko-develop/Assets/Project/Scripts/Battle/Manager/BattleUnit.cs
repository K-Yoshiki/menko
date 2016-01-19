using System.Collections.Generic;
using System.Linq;
using AppUtils.Assets;

namespace MenkoiMonster.Battle
{
	/// <summary>
	/// Battleのメンコパーティ編成データ
	/// </summary>
	public class BattleUnit
	{
		MenkoBattleData[] menkoDataList;

		public BattleUnit(uint[] monsterIDList, bool[] isRepresents, bool isPlayer)
		{
			int length = monsterIDList.Length;
			menkoDataList = new MenkoBattleData[length];
			for (int i = 0; i < length; ++i)
			{
				menkoDataList[i] = new MenkoBattleData(monsterIDList[i], isPlayer, isRepresents[i]);
			}
		}

		public MenkoBattleData GetData(int num)
		{
			return menkoDataList[num];
		}

		public MenkoBattleData[] GetData()
		{
			return menkoDataList;
		}

		public int GetIndex(MenkoBattleData data)
		{
			for (int i = 0; i < menkoDataList.Length; ++i)
			{
				if (data == menkoDataList[i])
				{
					return i;
				}
			}
			return -1;
		}

		public MenkoBattleData[] GetAttackerData()
		{
			List<MenkoBattleData> list = new List<MenkoBattleData>();
			menkoDataList.Foreach(data => {
				if (data.IsRepresent == false)
				{
					list.Add(data);
				}
			});
			return list.ToArray();
		}

		public MenkoBattleData[] GetReperesentData()
		{
			List<MenkoBattleData> list = new List<MenkoBattleData>();
			menkoDataList.Foreach(data => {
				if (data.IsRepresent)
				{
					list.Add(data);
				}
			});
			return list.ToArray();
		}

		public AssetLoadPath[] GetPreLoadPaths()
		{
			IEnumerable<AssetLoadPath> paths = new AssetLoadPath[] { };
			for (int i = 0; i < menkoDataList.Length; ++i)
			{
				paths = paths.Concat(menkoDataList[i].GetPreLoadPaths());
			}
			return paths.ToArray();
		}
	}
}