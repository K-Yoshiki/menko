using UnityEngine;
using System.Collections;
using AppUtils;
using AppUtils.Assets;

namespace MenkoiMonster.Scene
{
	public class TitleScene : SceneBase
	{
		public override void Init(StateMediator<SceneName> mediator)
		{
			Debug.Log("Start Title Scene");
		}

		public override void Update(StateMediator<SceneName> mediator)
		{

		}

		public override void Exit(StateMediator<SceneName> mediator)
		{

		}

		public override SceneName GetKey()
		{
			return SceneName.Title;
		}

		/// <summary>
		/// このシーンに使用されるシーン名を取得する
		/// </summary>
		/// <returns>The load scene names.</returns>
		public override string[] GetLoadSceneNames()
		{
			return new string[] { "Title" };
		}

		/// <summary>
		/// このシーンで使用されるアセットパス群を取得する
		/// </summary>
		/// <returns>The asset load paths.</returns>
		public override AssetLoadPath[] GetAssetLoadPaths()
		{
			return new AssetLoadPath[0];
		}
	}
}