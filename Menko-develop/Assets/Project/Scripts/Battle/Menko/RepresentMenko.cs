using System;
using UnityEngine;
using AppUtils;
using AppUtils.Assets;
using MenkoiMonster;

namespace MenkoiMonster.Battle
{
	public class RepresentMenko : Menko
	{
		AudioClip hitClip;

		void Awake()
		{
			var assetData = AssetManager.Load(AssetPath.SEPath + SEConst.Hit_NoneAttack);
			if (assetData.IsNull == false)
			{
				hitClip = (AudioClip)assetData.Asset;
			}
		}

		protected override void CollisionEnter(Collision enter)
		{
			// HitSEの再生
			Sound.Instance.PlaySE(hitClip);
		}
	}
}