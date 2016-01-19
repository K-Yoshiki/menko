using UnityEngine;
using AppUtils;
using AppUtils.Assets;
using MenkoiMonster.Home;

namespace MenkoiMonster.Scene
{
	public class HomeScene : SceneBase
	{
		HomeSceneManager homeManager;

		public override void Init(StateMediator<SceneName> mediator)
		{
			Debug.Log("Start Home Scene");
			homeManager = GameObject.FindObjectOfType<HomeSceneManager>();
//			homeManager.StartFadeIn();
		}

		public override void Update(StateMediator<SceneName> mediator)
		{

		}

		public override void Exit(StateMediator<SceneName> mediator)
		{

		}

		public override SceneName GetKey()
		{
			return SceneName.Home;
		}

		public override string[] GetLoadSceneNames()
		{
			return new string[]{ "Home" };
		}

		public override AssetLoadPath[] GetAssetLoadPaths()
		{
			return new AssetLoadPath[] { };
		}
	}
}