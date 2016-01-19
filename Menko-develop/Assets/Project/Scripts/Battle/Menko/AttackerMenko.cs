using UnityEngine;
using AppUtils;
using AppUtils.Assets;

namespace MenkoiMonster.Battle
{
	public class AttackerMenko : Menko
	{
		bool isAttack;
		bool isUseSkill;
		AudioClip hitClip;
		AudioClip attackClip;

		void Awake()
		{
			var assetData = AssetManager.Load(AssetPath.SEPath + SEConst.Hit_Attack);
			if (assetData.IsNull == false)
			{
				attackClip = (AudioClip)assetData.Asset;
			}

			assetData = AssetManager.Load(AssetPath.SEPath + SEConst.Hit_NoneAttack);
			if (assetData.IsNull == false)
			{
				hitClip = (AudioClip)assetData.Asset;
			}
		}

		public void StartAttack(Vector3 pos, Vector3 rot, Vector3 accel, bool isUseSkill)
		{
			isAttack = true;
			this.isUseSkill = isUseSkill;
			components.position = pos;
			FallForce(accel, rot);
		}

		public void EndAttack()
		{
			isAttack = false;
		}

		void FallForce(Vector3 vector, Vector3 rotation)
		{
			rotation.x -= 90;
			components.eulerAngles = rotation;
			vector.Normalize();
			vector.y = 1;
			components.AddForce(vector * -1f, ForceMode.VelocityChange);
		}

		protected override void CollisionEnter(Collision enter)
		{
			// HitSEの再生
			Sound.Instance.PlaySE(hitClip);

			if (!isAttack)
			{
				return;
			}

			// スキル発動
			if (isUseSkill)
			{
				Debug.Log("UsingSkill");
				isUseSkill = false;
				skillController.InvokeSkill(data, this);
			}

			if (enter.gameObject.tag != "Monster")
			{
				return;
			}

			Menko opponent = enter.gameObject.GetComponent<Menko>();
			if (opponent.IsPlayer())
			{
				return;
			}

			isAttack = false;

			// ダメージを相手に与える
			opponent.Damage(this.data.Status, 1.0f); // TODO: 位置倍率計算を入れる

			// 攻撃エフェクトの発生(弱点の考慮)
			var effect = Instantiate(ResourceUtils.GetMenkoHitEffect(data.Status.Element));
			effect.transform.position = enter.contacts[0].point + Vector3.up * 0.25f;
			Destroy(effect.gameObject, effect.Duration);

			// 攻撃SEの再生
			Sound.Instance.PlaySE(attackClip);
		}
	}
}