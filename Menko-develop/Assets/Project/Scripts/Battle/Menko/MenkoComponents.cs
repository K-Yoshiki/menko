using UnityEngine;
using System.Collections;

namespace MenkoiMonster.Battle
{
	public struct MenkoComponents
	{
		Transform m_transform;
		Rigidbody m_rigidbody;
		Vector3 velocity;
		Vector3 torque;

		public MenkoComponents(Menko menko, MenkoBattleData data)
		{
			m_transform = menko.GetComponent<Transform>();
			m_rigidbody = menko.GetComponent<Rigidbody>();
			velocity = m_rigidbody.velocity;
			torque = m_rigidbody.angularVelocity;
			SetMaterial(data);
			SetColor(data);
		}

		public Vector3 position
		{
			get { return m_transform.position; }
			set { m_transform.position = value; }
		}

		public Vector3 eulerAngles
		{
			get { return m_transform.eulerAngles; }
			set { m_transform.eulerAngles = value; }
		}

		public float RotY
		{
			get { return eulerAngles.y; }
			set { m_transform.SetRotY(value); }
		}

		public Vector3 scale
		{
			get { return m_transform.localScale; }
			set { m_transform.localScale = value; }
		}

		void SetMaterial(MenkoBattleData data)
		{
			var renderer = m_transform.GetComponent<MeshRenderer>();
			var mats = renderer.sharedMaterials;
			mats[1] = ResourceUtils.GetFaceMat(data.BaseData.ID);
			mats[0] = ResourceUtils.GetBackMat(data.BaseData.Status.Element);
			renderer.sharedMaterials = mats;
		}

		void SetColor(MenkoBattleData data)
		{
			var colorRing = m_transform.GetComponentInChildren<SpriteRenderer>();
			colorRing.enabled = data.IsRepresent;
			if (data.IsPlayer)
				colorRing.color = BattleConst.Menko.PlayerColor;
			else
				colorRing.color = BattleConst.Menko.RivalColor;
		}

		public void AddForce(Vector3 force, ForceMode mode)
		{
			m_rigidbody.AddForce(force, mode);
		}

		public void AddTorque(Vector3 torque, ForceMode mode)
		{
			m_rigidbody.AddTorque(torque, mode);
		}

		public bool IsSleeping()
		{
			return m_rigidbody.IsSleeping();
		}

		public void StopRigid()
		{
			if (m_rigidbody.isKinematic == false)
			{
				velocity = m_rigidbody.velocity;
				torque = m_rigidbody.angularVelocity;
				m_rigidbody.isKinematic = true;
			}
		}

		public void ResumeRigid()
		{
			if (m_rigidbody.isKinematic)
			{
				m_rigidbody.isKinematic = false;
				m_rigidbody.velocity = velocity;
				m_rigidbody.angularVelocity = torque;
			}
		}
	}
}