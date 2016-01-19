namespace MenkoiMonster
{
	/// <summary>
	/// アセットのリソースフォルダパス
	/// </summary>
	public static class AssetPath
	{
		/// <summary>
		/// BGMのフォルダパス
		/// </summary>
		public const string BGMPath = "Sounds/BGM/";

		/// <summary>
		/// SEのフォルダパス
		/// </summary>
		public const string SEPath = "Sounds/SE/";

		/// <summary>
		/// Jingleのフォルダパス
		/// </summary>
		public const string JinglePath = "Sounds/Jingle";

		/// <summary>
		/// エフェクトのフォルダパス
		/// </summary>
		public const string EffectPath = "Effect/";

		/// <summary>
		/// ヒットエフェクトのフォルダパス
		/// </summary>
		public const string HitEffectPath = EffectPath + "Menko/Attack/Hit/";

		/// <summary>
		/// 弱点エフェクトのフォルダパス
		/// </summary>
		public const string WeakEffectPath = EffectPath + "Menko/Attack/Weak/";

		/// <summary>
		/// ロード画面のパス
		/// </summary>
		const string LoadScreenPath = "Loading/LoadingScreen";

		/// <summary>
		/// ID指定時のフォーマット
		/// </summary>
		const string IDFormat = "0000";

		/// <summary>
		/// モンスター基本データのフォルダパス
		/// </summary>
		const string MonsterDataPath = "Monsters/Data/";

		/// <summary>
		/// モンスタースキルデータのフォルダパス
		/// </summary>
		const string SkillDataPath = "Skill/Data/";

		/// <summary>
		/// モンスタースキルの実行プレハブのフォルダパス
		/// </summary>
		const string SkillPrefabPath = "Skill/Prefabs/";

		/// <summary>
		/// バトルステージデータの基本パス
		/// </summary>
		const string StagePrefabPath = "Stage/Prefabs/Map_";

		/// <summary>
		/// メンコ表面マテリアルのフォルダパス
		/// </summary>
		const string MenkoFaceMatPath = "Monsters/Materials/Face/";

		/// <summary>
		/// メンコ裏面マテリアルのフォルダパス
		/// </summary>
		const string MenkoBackMatPath = "Monsters/Materials/Back/";

		/// <summary>
		/// モンスターのメンコ表面テクスチャのフォルダパス
		/// </summary>
		const string MonsterFaceTexPath = "Monsters/Textures/Face/";

		/// <summary>
		/// モンスターの全体像テクスチャのフォルダパス
		/// </summary>
		const string MonsterFullTexPath = "Monsters/Textures/Full/";

		/// <summary>
		/// ロード画面のプレハブのパスを取得する
		/// </summary>
		/// <returns>The load screen path.</returns>
		public static string GetLoadScreenPath()
		{
			return LoadScreenPath;
		}

		/// <summary>
		/// モンスター基本データのパスを取得する
		/// </summary>
		/// <returns>The monster data path.</returns>
		/// <param name="id">Identifier.</param>
		public static string GetMonsterDataPath(uint id)
		{
			return MonsterDataPath + id.ToString(IDFormat);
		}

		/// <summary>
		/// モンスタースキルデータのパスを取得する
		/// </summary>
		/// <returns>The skill data path.</returns>
		/// <param name="id">Identifier.</param>
		public static string GetSkillDataPath(uint id)
		{
			return SkillDataPath + id.ToString(IDFormat);
		}

		/// <summary>
		/// モンスタースキル実行プレハブのパスを取得する
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static string GetSkillPrefabPath(uint id)
		{
			return SkillPrefabPath + id.ToString(IDFormat);
		}

		/// <summary>
		/// ステージIDからステージデータを取得する
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static string GetStagePrefabPath(uint id)
		{
			return StagePrefabPath + id.ToString(IDFormat);
		}

		/// <summary>
		/// メンコの表面マテリアルのパスを取得する
		/// </summary>
		/// <returns>The menko face mat path.</returns>
		/// <param name="id">Identifier.</param>
		public static string GetMenkoFaceMatPath(uint id)
		{
			return MenkoFaceMatPath + id.ToString(IDFormat);
		}

		/// <summary>
		/// メンコの裏面マテリアルのパスを取得する
		/// </summary>
		/// <returns>The menko back mat path.</returns>
		/// <param name="element">Element.</param>
		public static string GetMenkoBackMatPath(MenkoElement element)
		{
			switch (element)
			{
				case MenkoElement.Flame:
					return MenkoBackMatPath + "Flame";
				case MenkoElement.Water:
					return MenkoBackMatPath + "Water";
				case MenkoElement.Leaf:
					return MenkoBackMatPath + "Leaf";
				case MenkoElement.Light:
					return MenkoBackMatPath + "Light";
				case MenkoElement.Dark:
					return MenkoBackMatPath + "Dark";
				default:
					return MenkoBackMatPath + "None";
			}
		}

		/// <summary>
		/// モンスターのメンコ表面テクスチャのパスを取得する
		/// </summary>
		/// <returns>The monster face tex path.</returns>
		/// <param name="id">Identifier.</param>
		public static string GetMonsterFaceTexPath(uint id)
		{
			return MonsterFaceTexPath + id.ToString(IDFormat);
		}

		/// <summary>
		/// モンスターの全体像テクスチャのパスを取得する
		/// </summary>
		/// <returns>The monster full tex path.</returns>
		/// <param name="id">Identifier.</param>
		public static string GetMonsterFullTexPath(uint id)
		{
			return MonsterFullTexPath + id.ToString(IDFormat);
		}
	}
}