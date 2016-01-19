using UnityEngine;
using System;
using AppUtils;
using AppUtils.Assets;

namespace MenkoiMonster.Battle
{
	public abstract class Menko : MonoBehaviour
	{
		protected MenkoComponents components;
		protected MenkoBattleData data;
		protected SkillController skillController;

		public static Menko CreateInstance(MenkoBattleData battleData, SkillController skillController, Vector3 pos)
		{
			var instance = Instantiate(AssetManager.Load(BattleConst.Menko.BaseMenkoPath).Asset) as GameObject;

			Menko result;
			if (battleData.IsRepresent)
			{
				result = instance.AddComponent<RepresentMenko>();
			}
			else
			{
				result = instance.AddComponent<AttackerMenko>();
			}

			result.SetUp(battleData, skillController, pos);
			return result;
		}

		public void SetUp(MenkoBattleData data, SkillController skillController, Vector3 pos)
		{
			this.data = data;
			this.skillController = skillController;
			components = new MenkoComponents(this, data);
			gameObject.SetActive(data.IsRepresent);
			components.position = pos;
			components.RotY = data.IsPlayer ? 180f : 0f;
			components.scale = Vector3.one * BattleConst.Menko.SizeToScale(data.BaseData.Character.Size);
		}

		public void Damage(BattleStatus status, float posMagnif)
		{
			data.Status.Damage(status, posMagnif, transform.position);
			components.AddForce(Vector3.up * 0.01f * data.Status.Percent, ForceMode.Impulse);
			components.AddTorque(Vector3.right * 0.1f * data.Status.Percent, ForceMode.Impulse);
		}

		public void SkillDamage(uint damage, MenkoElement element)
		{
			data.Status.SlipDamage(damage, element, transform.position);
		}

		public bool IsDead()
		{
			float x = components.eulerAngles.x;
			if (x < BattleConst.Menko.DefeatXMin)
				return false;
			if (x > BattleConst.Menko.DefeatXMax)
				return false;
			return true;
		}

		public void SetKinematic(bool isKinematic)
		{
			if (isKinematic)
				components.StopRigid();
			else
				components.ResumeRigid();
		}

		/// <summary>
		/// 死亡状態にする
		/// </summary>
		public void Difeat()
		{
			data.Status.IsDead = true;
		}

		public bool IsOutMap()
		{
			if (components.position.y < BattleConst.Map.FloorHeight)
				return true;
			return false;
		}

		public bool IsPlayer()
		{
			return data.IsPlayer;
		}

		public bool IsRepresent()
		{
			return data.IsRepresent;
		}

		public bool IsSleep()
		{
			return components.IsSleeping();
		}

		protected abstract void CollisionEnter(Collision enter);

		void OnCollisionEnter(Collision enter)
		{
			CollisionEnter(enter);
		}
	}
}