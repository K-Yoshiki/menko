using UnityEngine;
using System;

namespace MenkoiMonster
{
	/// <summary>
	/// メンコのキャラクター情報
	/// </summary>
	[Serializable]
	public class MenkoCharacter
	{
		[SerializeField] string charaName;
		[SerializeField] ushort skillID;
		[SerializeField] MenkoSize size;
		[SerializeField, Multiline(5)] string charaInfo;

		/// <summary>
		/// キャラクター名
		/// </summary>
		public string CharaName { get { return charaName; } }

		/// <summary>
		/// キャラクター説明文
		/// </summary>
		public string CharaInfo { get { return charaInfo; } }

		/// <summary>
		/// 指定スキル番号
		/// </summary>
		public ushort SkillID { get { return skillID; } }

		/// <summary>
		/// メンコのサイズ
		/// </summary>
		public MenkoSize Size { get { return size; } }

		public MenkoCharacter(string charaName, ushort skillID, MenkoSize size, string charaInfo)
		{
			this.charaName = charaName;
			this.skillID = skillID;
			this.size = size;
			this.charaInfo = charaInfo;
		}
	}
}