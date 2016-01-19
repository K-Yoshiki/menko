using UnityEngine;
using AppUtils.Assets;
using MenkoiMonster.Battle;

namespace MenkoiMonster
{
	public static class ResourceUtils
	{
		public static Material GetFaceMat(uint id)
		{
			var path = AssetPath.GetMenkoFaceMatPath(id);
			var data = AssetManager.Load<Material>(path);
			if (!data.IsNull)
			{
				return data.Asset as Material;
			}
			return null;
		}

		public static Material GetBackMat(MenkoElement element)
		{
			var path = AssetPath.GetMenkoBackMatPath(element);
			var data = AssetManager.Load<Material>(path);
			if (!data.IsNull)
			{
				return data.Asset as Material;
			}
			return null;
		}

		public static Sprite GetFaceSprite(uint id)
		{
			var path = AssetPath.GetMonsterFaceTexPath(id);
			var data = AssetManager.Load<Sprite>(path);
			if (!data.IsNull)
			{
				return data.Asset as Sprite;
			}
			return null;
		}

		public static Sprite GetFullSprite(uint id)
		{
			var path = AssetPath.GetMonsterFullTexPath(id);
			var data = AssetManager.Load<Sprite>(path);
			if (!data.IsNull)
			{
				return data.Asset as Sprite;
			}
			return null;
		}

		public static Effect GetMenkoReturnEffect()
		{
			var path = AssetPath.EffectPath + "Menko/Special/MenkoReturn";
			var data = AssetManager.Load<Effect>(path);
			if (!data.IsNull)
			{
				return data.Asset as Effect;
			}
			return null;
		}

		public static Effect GetMenkoHitEffect(MenkoElement element)
		{
			var path = AssetPath.HitEffectPath + "Hit_" + element.ToString();
			var data = AssetManager.Load<Effect>(path);
			if (!data.IsNull)
			{
				return data.Asset as Effect;
			}
			return null;
		}

		public static Effect GetMenkoWeakHitEffect(MenkoElement element)
		{
			var path = AssetPath.WeakEffectPath + element.ToString();
			var data = AssetManager.Load<Effect>(path);
			if (!data.IsNull)
			{
				return data.Asset as Effect;
			}
			return null;
		}

		public static Effect GetMenkoAttackEffect(MenkoElement atk, MenkoElement dif)
		{
			return GetMenkoHitEffect(atk);
		}
	}
}