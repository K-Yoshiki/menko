using UnityEngine;
using System.Collections;

namespace MenkoiMonster.Battle
{
	public class DecideMenko : MonoBehaviour
	{
		Transform m_transform;
		Rigidbody m_rigidbody;

		void Awake()
		{
			m_transform = transform;
			m_rigidbody = GetComponent<Rigidbody>();
			m_rigidbody.isKinematic = true;
		}

		public void Flipping(float addForce, float addTorque)
		{
			m_rigidbody.isKinematic = false;
			m_rigidbody.AddForce(Vector3.up * addForce, ForceMode.VelocityChange);
			m_rigidbody.AddTorque(Vector3.right * addTorque, ForceMode.VelocityChange);
		}

		public bool IsBack()
		{
			float x = m_transform.eulerAngles.x;
			if (x < BattleConst.Menko.DefeatXMin)
				return false;
			if (x > BattleConst.Menko.DefeatXMax)
				return false;
			return true;
		}

		public bool IsSleep()
		{
			return m_rigidbody.IsSleeping();
		}
	}
}