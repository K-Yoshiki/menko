using AppUtils.Assets;

namespace MenkoiMonster.Battle
{
	public class MenkoBattleData
	{
		MenkoData baseData;
		SkillData skillData;
		BattleStatus status;
		bool isPlayer;
		bool isRepresent;

		public MenkoBattleData(uint id, bool isPlayer, bool isRepresent)
		{
			this.isRepresent = isRepresent;
			this.isPlayer = isPlayer;
			baseData = AssetManager.Load(AssetPath.GetMonsterDataPath(id)).Asset as MenkoData;
			skillData = AssetManager.Load(AssetPath.GetSkillDataPath(baseData.Character.SkillID)).Asset as SkillData;
			status = new BattleStatus(baseData.Status, skillData);
		}

		public MenkoData BaseData
		{
			get { return baseData; }
		}

		public BattleStatus Status
		{
			get { return status; }
		}

		public SkillData SkillData
		{
			get { return skillData; }
		}

		public UnitVM UnitViewModel
		{
			set { status.UnitViewModel = value; }
		}

		/// <summary>
		/// 代表格メンコか
		/// </summary>
		public bool IsRepresent
		{
			get { return isRepresent; }
		} 
			
		/// <summary>
		/// プレイヤーのメンコか
		/// </summary>
		public bool IsPlayer
		{
			get { return isPlayer; }
		}

		/// <summary>
		/// スキルを発動出来るか
		/// </summary>
		public bool CanUseSkill
		{
			get { return status.SkillEnergy >= skillData.NeedTurn; }
		}

		public AssetLoadPath[] GetPreLoadPaths()
		{
			return baseData.GetBattlePreLoadPaths();
		}
	}
}