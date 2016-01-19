using UnityEngine;
using System;

namespace MenkoiMonster
{
	/// <summary>
	/// スキル情報クラス
	/// </summary>
	[CreateAssetMenu(menuName = "Skill Data Prefab", order = 60)]
	public class SkillData : ScriptableObject
	{
		[SerializeField]
		ushort identifier;

		[SerializeField]
		ushort useSkillID;

		[SerializeField]
		string skillName;

		[SerializeField, Multiline]
		string skillInfo;

		[SerializeField]
		SkillParameters parameters;

		/// <summary>
		/// スキルデータ固有番号
		/// </summary>
		public ushort ID { get { return identifier; } }

		/// <summary>
		/// 使用するスキル実体ID
		/// </summary>
		public ushort UseSkillID { get { return useSkillID; } }

		/// <summary>
		/// スキル属性
		/// </summary>
		public MenkoElement Element { get { return parameters.Element; } }

		/// <summary>
		/// 必要ターン数
		/// </summary>
		/// <value>The need turnes.</value>
		public byte NeedTurn { get { return parameters.NeedTurn; } }

		/// <summary>
		/// 持続ターン数（持続系スキルのみ）
		/// </summary>
		public byte DurationTurn { get { return parameters.DurationTurn; } }

		/// <summary>
		/// ダメージもしくは回復、乗算元などの影響数値
		/// </summary>
		public float[] Values { get { return parameters.Values; } }

		/// <summary>
		/// スキル名
		/// </summary>
		public string Name { get { return skillName; } }

		/// <summary>
		/// スキル説明文
		/// </summary>
		/// <value>The info.</value>
		public string Info
		{
			get
			{
				string text = skillInfo;
				for (int i = 0; i < parameters.Values.Length; ++i)
				{
					text = string.Format(text, parameters.Values[i]);
				}
				return text;
			}
		}
	}

	/// <summary>
	/// スキルの数値情報
	/// </summary>
	[Serializable]
	public struct SkillParameters
	{
		[SerializeField]
		MenkoElement element;

		[SerializeField]
		byte needTurn;

		[SerializeField]
		byte durationTurn;

		[SerializeField]
		float[] values;
		
		/// <summary>
		/// スキル属性
		/// </summary>
		public MenkoElement Element { get { return element; } }

		/// <summary>
		/// 必要ターン数
		/// </summary>
		/// <value>The need turn.</value>
		public byte NeedTurn { get { return needTurn; } }

		/// <summary>
		/// 持続ターン数（持続系スキルのみ）
		/// </summary>
		public byte DurationTurn { get { return durationTurn; } }

		/// <summary>
		/// ダメージもしくは回復、乗算元などの影響数値
		/// </summary>
		public float[] Values { get { return values; } }
	}
}