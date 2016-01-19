using UnityEngine;

namespace MenkoiMonster.Battle
{
	public class BattleStatus
	{
		struct BattleParams
		{
			public uint attack;
			public uint difense;
		}

		MenkoStatus status;
		SkillData skillData;
		BattleParams variables;
		UnitVM viewModel;
		int percent;
		int skillEnergy;
		bool isDead;

		public BattleStatus(MenkoStatus status, SkillData skillData)
		{
			this.status = status;
			this.skillData = skillData;
			this.percent = 0;
			this.skillEnergy = 0;
			this.isDead = false;
			variables = new BattleParams();
		}

		public UnitVM UnitViewModel
		{
			set { viewModel = value; }
		}

		/// <summary>
		/// 攻撃力
		/// </summary>
		public uint Attack
		{
			get { return status.Attack + VariableAttack; }
		}

		/// <summary>
		/// 加算分攻撃力
		/// </summary>
		public uint VariableAttack
		{
			private get { return variables.attack; }
			set { variables.attack = value; }
		}

		/// <summary>
		/// 防御力
		/// </summary>
		public uint Difense
		{
			get { return status.Difense + VariableAttack; }
		}

		/// <summary>
		/// 加算分防御力
		/// </summary>
		public uint VariableDifense
		{
			private get { return variables.difense; }
			set { variables.difense = value; }
		}

		/// <summary>
		/// メンコの属性
		/// </summary>
		public MenkoElement Element
		{
			get { return status.Element; }
		}

		/// <summary>
		/// ダメージ率
		/// </summary>
		public int Percent
		{
			get { return percent; }
			private set
			{
				percent = value;
				viewModel.Persent = percent.ToString();
			}
		}

		/// <summary>
		/// スキル発動のための蓄積ターン数
		/// </summary>
		/// <value>The skill turn.</value>
		public int SkillEnergy
		{
			get { return skillEnergy; }
			private set
			{
				skillEnergy = value;
				int remainingTurn = skillData.NeedTurn - skillEnergy;
				viewModel.SkillTurn = remainingTurn <= 0 ? "OK" : remainingTurn.ToString();
			}
		}

		public bool IsDead
		{
			get { return isDead; }
			set
			{
				isDead = value;
				viewModel.IsDead = isDead;
			}
		}

		/// <summary>
		/// ダメージ加算
		/// </summary>
		public void Damage(BattleStatus opStatus, float posMagnif, Vector3 pos)
		{
			const int LV = 100; // 今のところ固定
			float elementMag = MenkoElementUtils.GetMagnification(opStatus.Element, this.Element);
			float attackBase = (LV * 0.4f + 2) * opStatus.Attack * elementMag;
			float difensed = attackBase / (this.Difense * 0.75f);
			int damage = Mathf.CeilToInt(difensed * posMagnif);
			Percent += damage;
			DamageToaster.Instance.ShowDamage(damage, pos);
		}

		/// <summary>
		/// 固定ダメージ加算(属性考慮)
		/// </summary>
		/// <param name="damage"></param>
		/// <param name="element"></param>
		public void SlipDamage(uint damage, MenkoElement element, Vector3 pos)
		{
			float elementMagnif = MenkoElementUtils.GetMagnification(element, this.Element);
			int lastDamage = Mathf.CeilToInt(damage * elementMagnif);
			Percent += lastDamage;
			DamageToaster.Instance.ShowDamage(lastDamage, pos);
		}

		/// <summary>
		/// ターン経過
		/// </summary>
		public void ElapseTurn()
		{
			this.SkillEnergy++;
		}

		/// <summary>
		/// スキルエネルギーの使用
		/// </summary>
		public void ResetSkillEnergy()
		{
			this.SkillEnergy = 0;
		}
	}
}