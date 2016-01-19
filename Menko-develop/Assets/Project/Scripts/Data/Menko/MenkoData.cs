using UnityEngine;
using System.Collections.Generic;
using AppUtils.Assets;

namespace MenkoiMonster
{
	/// <summary>
	/// メンコの基礎データを格納するクラス
	/// </summary>
	[CreateAssetMenu(menuName = "Menko Data Prefab", order = 60)]
	public class MenkoData : ScriptableObject
	{
		[SerializeField] ushort identifier;
		[SerializeField] ushort cost;
		[SerializeField] MenkoCharacter character;
		[SerializeField] MenkoStatus status;

		/// <summary>
		/// メンコキャラの固有番号
		/// </summary>
		public ushort ID { get { return identifier; } }

		/// <summary>
		/// キャラのコスト
		/// </summary>
		/// <value>The const.</value>
		public ushort Const { get { return cost; } }

		/// <summary>
		/// メンコの名称など
		/// </summary>
		public MenkoCharacter Character { get { return character; } }

		/// <summary>
		/// バトル関連の計算用パラメータ
		/// </summary>
		public MenkoStatus Status { get { return status; } }

		public MenkoData(ushort identifier, MenkoCharacter character, MenkoStatus parameters)
		{
			this.identifier = identifier;
			this.character = character;
			this.status = parameters;
		}

		public AssetLoadPath[] GetMenuPreLoadPaths()
		{
			var paths = new AssetLoadPath[3];
			paths[0] = new AssetLoadPath(AssetPath.GetMonsterFaceTexPath(this.ID), typeof(Sprite));
			paths[1] = new AssetLoadPath(AssetPath.GetSkillDataPath(this.ID));
			paths[2] = new AssetLoadPath(AssetPath.GetMonsterFullTexPath(this.ID), typeof(Sprite));
			return paths;
		}

		public AssetLoadPath[] GetBattlePreLoadPaths()
		{
			var paths = new AssetLoadPath[4];
			paths[0] = new AssetLoadPath(AssetPath.GetMonsterFaceTexPath(this.ID), typeof(Sprite));
			paths[1] = new AssetLoadPath(AssetPath.GetMenkoFaceMatPath(this.ID), typeof(Material));
			paths[2] = new AssetLoadPath(AssetPath.GetMenkoBackMatPath(this.Status.Element), typeof(Material));
			paths[3] = new AssetLoadPath(AssetPath.GetMonsterFullTexPath(this.ID), typeof(Sprite));
			return paths;
		}
	}
}