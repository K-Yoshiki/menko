using UnityEngine;
using System;
using System.Collections;

namespace MenkoiMonster
{
	public class UserData
	{
		UnitData unit;
		RecordData records;

		public UserData()
		{
			unit = new UnitData();
			records = new RecordData();
		}

		public UnitData Unit
		{
			get { return unit; }
		}

		public RecordData Record
		{
			get { return records; }
		}
	}

	/// <summary>
	/// 編成データ
	/// </summary>
	public class UnitData
	{
		uint[] monstersID;

		public UnitData()
		{
			monstersID = new uint[5];
		}

		public uint this[int index]
		{
			get { return monstersID[index]; }
			set { monstersID[index] = value; }
		}
	}

	public class RecordData
	{
		
	}
}