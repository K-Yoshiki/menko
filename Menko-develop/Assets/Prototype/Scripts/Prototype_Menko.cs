using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Prototype;
using MenkoiMonster.Battle;
using AppUtils;

public class Prototype_Menko : MonoBehaviour
{
	[SerializeField] Transform selfTf;
	[SerializeField] MeshRenderer m_meshRenderer;
	[SerializeField] Rigidbody m_rigidbody;
	[SerializeField] Prototype_MenkoCollider menkoCollider;
	[SerializeField] Effect attackEffect;
	[SerializeField] Effect deadEffect;
	[SerializeField] AudioClip clip;
	[SerializeField, Range(0, 360)] float deadThresholdAngle;
	bool isAttack;

	void Awake()
	{
		selfTf = selfTf ?? transform;
		this.menkoCollider.CollisionEnterEvent += OnCollisionEnter;
	}

	public void SetMaterial(Material material)
	{
		Material[] mats = m_meshRenderer.sharedMaterials;
		mats[1] = material;
		m_meshRenderer.sharedMaterials = mats;
	}

	public void StartAttack(Vector3 position, Vector3 rotation, Vector3 vector)
	{
		this.selfTf.position = position;
		this.AddForce(vector, rotation);
		this.isAttack = true;
	}

	public void EndAttack()
	{
		this.isAttack = false;
	}

	public bool IsDead()
	{
		float x = -90;
		float xMin = Mathf.Repeat(x + 180.0f - (deadThresholdAngle * 0.5f), 360f);
		float xMax = Mathf.Repeat(x + 180.0f + (deadThresholdAngle * 0.5f), 360f);

		x = this.menkoCollider.transform.eulerAngles.x;
		if(x < xMin)
		{
			return false;
		}
		if (x > xMax)
		{
			return false;
		}
		return true;
	}

	void AddForce(Vector3 vector, Vector3 rotation)
	{
		vector.Normalize();
		vector.y = 1f;
		selfTf.eulerAngles = rotation;
		m_rigidbody.AddForce(vector * -1, ForceMode.VelocityChange);
	}

	void OnCollisionEnter(Collision col)
	{
		if (!isAttack)
			return;
		if (col.gameObject.tag != "Monster")
			return;

		Effect effect = Instantiate(attackEffect);
		col.gameObject.GetComponent<Rigidbody>().AddTorque(Vector3.right * 0.1f, ForceMode.VelocityChange);
		effect.SetPos(m_rigidbody.transform.position);
		Sound.Instance.PlaySE(clip);
		Destroy(effect.gameObject, effect.Duration);
	}
}