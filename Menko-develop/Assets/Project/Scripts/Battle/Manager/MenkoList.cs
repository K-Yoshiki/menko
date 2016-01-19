using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using AppUtils.Assets;

namespace MenkoiMonster.Battle
{
	/// <summary>
	/// バトル上で生成したメンコの管理
	/// </summary>
	public class MenkoList
	{
		MenkoGroup players;
		MenkoGroup rivals;

		public MenkoList()
		{
			players = new MenkoGroup();
			rivals = new MenkoGroup();
		}

		public Menko GetAttacker(int index, bool isPlayer)
		{
			return isPlayer ? players.GetAttacker(index) : rivals.GetAttacker(index);
		}

		public Menko GetRepresent(int index, bool isPlayer)
		{
			return isPlayer ? players.GetRepresent(index) : rivals.GetRepresent(index);
		}

		public Menko[] GetAttackerList(bool isPlayer)
		{
			return isPlayer ? players.Attackers : rivals.Attackers;
		}

		public Menko[] GetRepresentList(bool isPlayer)
		{
			return isPlayer ? players.Represents : rivals.Represents;
		}

		public Menko[] GetAttackerList()
		{
			return players.Attackers.Concat(rivals.Attackers).ToArray();
		}

		public Menko[] GetRepresentList()
		{
			return players.Represents.Concat(rivals.Represents).ToArray();
		}

		public Menko[] GetAll(bool isPlayer)
		{
			return isPlayer ? players.All : rivals.All;
		}

		public bool IsAllSleep()
		{
			return players.IsAllSleep && rivals.IsAllSleep;
		}

		public bool IsFinished()
		{
			return players.RepresentActives <= 0 || rivals.RepresentActives <= 0;
		}

		public void Add(Menko menko)
		{
			if (menko.IsPlayer())
			{
				players.Add(menko);
			}
			else
			{
				rivals.Add(menko);
			}
		}

		public void Remove(Menko menko)
		{
			if (menko.IsPlayer())
			{
				players.Remove(menko);
			}
			else
			{
				rivals.Remove(menko);
			}
		}

		public int GetPlayerIndex(Menko menko)
		{
			Menko[] playersAll = players.All;
			for (int i = 0; i < playersAll.Length; i++)
			{
				if (playersAll[i] == menko)
				{
					return i;
				}
			}
			return -1;
		}

		public int PlayerRepresentActives
		{
			get { return players.RepresentActives; }
		}

		public int RivalRepresentActives
		{
			get { return rivals.RepresentActives; }
		}

		/// <summary>
		/// 全てのメンコの物理挙動を止める
		/// </summary>
		public void StopRigidAll()
		{
			players.All.Foreach(menko => menko.SetKinematic(true));
			rivals.All.Foreach(menko => menko.SetKinematic(true));
		}

		/// <summary>
		/// 全てのメンコの物理挙動を再開させる
		/// </summary>
		public void ResumeRigidAll()
		{
			players.All.Foreach(menko => menko.SetKinematic(false));
			rivals.All.Foreach(menko => menko.SetKinematic(false));
		}

		#region MenkoList in class

		class MenkoGroup
		{
			List<Menko> all;
			List<Menko> attackers;
			List<Menko> represents;

			public MenkoGroup()
			{
				all = new List<Menko>();
				attackers = new List<Menko>();
				represents = new List<Menko>();
			}

			/// <summary>
			/// 生成された攻撃用メンコ
			/// </summary>
			/// <value>The attakers.</value>
			public Menko[] Attackers
			{
				get { return attackers.ToArray(); }
			}

			/// <summary>
			/// 生成された攻撃用メンコの取得
			/// </summary>
			/// <returns>The attaker.</returns>
			/// <param name="index">Index.</param>
			public Menko GetAttacker(int index)
			{
				return attackers[index];
			}

			/// <summary>
			/// 生成された代表格メンコ
			/// </summary>
			/// <value>The represents.</value>
			public Menko[] Represents
			{
				get { return represents.ToArray(); }
			}

			/// <summary>
			/// 生成された代表格メンコの取得
			/// </summary>
			/// <returns>The represent.</returns>
			/// <param name="index">Index.</param>
			public Menko GetRepresent(int index)
			{
				return represents[index];
			}

			/// <summary>
			/// 全メンコ取得
			/// </summary>
			/// <value>All.</value>
			public Menko[] All
			{
				get { return all.ToArray(); }
			}

			/// <summary>
			/// 現在アクティブ状態の攻撃用メンコの数
			/// </summary>
			/// <value>The attacker count.</value>
			public int AttackerActives
			{
				get { return attackers.FindAll(menko => menko.gameObject.activeSelf).Count; }
			}

			/// <summary>
			/// 現在アクティブ状態の代表格メンコの数
			/// </summary>
			/// <value>The represent count.</value>
			public int RepresentActives
			{
				get { return represents.FindAll(menko => menko.gameObject.activeSelf).Count; }
			}

			public bool IsAllSleep
			{
				get { return attackers.All(IsSleep) && represents.All(IsSleep); }
			}

			public void Add(Menko menko)
			{
				if (menko.IsRepresent())
				{
					if (!represents.Contains(menko))
					{
						all.Add(menko);
						represents.Add(menko);
					}
					return;
				}
				else
				{
					if (!attackers.Contains(menko))
					{
						all.Add(menko);
						attackers.Add(menko);
					}
				}
			}

			public void Remove(Menko menko)
			{
				menko.gameObject.SetActive(false);
				var effect = Object.Instantiate(ResourceUtils.GetMenkoReturnEffect());
				effect.transform.position = menko.transform.position + Vector3.up * 0.2f;
				Object.Destroy(effect.gameObject, effect.Duration);
			}

			bool IsSleep(Menko menko)
			{
				return menko.IsSleep();
			}
		}

		#endregion
	}
}