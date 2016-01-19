using UnityEngine;
using System.Collections;
using AppUtils;
using AppUtils.Assets;
using MenkoiMonster;

public class PlaySE : MonoBehaviour
{
	public string resourcePath;

	public void Invoke()
	{
		AssetData assetData = AssetManager.Load(AssetPath.SEPath + resourcePath);
		if (assetData.IsNull == false)
		{
			Sound.Instance.PlaySE((AudioClip)assetData.Asset);
		}
	}
}