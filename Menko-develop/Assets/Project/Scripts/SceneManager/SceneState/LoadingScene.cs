using UnityEngine;
using System.Collections;
using AppUtils;
using AppUtils.Assets;

namespace MenkoiMonster.Scene
{
	public class LoadingScene : SceneBase
	{
		IState<SceneName> nextScene;
		SceneTransition sceneTrans;

		public LoadingScene(IState<SceneName> nextScene, SceneTransition sceneTrans)
		{
			this.nextScene = nextScene;
			this.sceneTrans = sceneTrans;
		}

		public override void Init(StateMediator<SceneName> mediator)
		{
			SceneManager.Instance.StartCoroutine(Load(mediator));
		}

		public override void Update(StateMediator<SceneName> mediator)
		{
		}

		public override void Exit(StateMediator<SceneName> mediator)
		{

		}

		public override SceneName GetKey()
		{
			return SceneName.Loading;
		}

		public override string[] GetLoadSceneNames()
		{
			return new string[] { "" };
		}

		public override AssetLoadPath[] GetAssetLoadPaths()
		{
			return new AssetLoadPath[0];
		}

		IEnumerator Load(StateMediator<SceneName> mediator)
		{
			// Loading画面の検索
			LoadingScreen screen;
			screen = GameObject.FindObjectOfType<LoadingScreen>();

			// Loading画面の生成
			if (screen == null)
			{
				var asset = AssetManager.Load(AssetPath.GetLoadScreenPath());
				screen = ((GameObject)GameObject.Instantiate(asset.Asset)).GetComponent<LoadingScreen>();
			}

			// 画面の遷移表現開始
			screen.ShowScreen();

			// 画面の遷移表現が終わるまで待つ
			while (screen.IsFading())
			{
				yield return null;
			}

			// 少しだけWait
			yield return new WaitForSeconds(0.5f);

			// Unityシーンの非同期ロード開始
			this.sceneTrans.Load();

			// Unityシーンのロード進捗を待つ
			while (this.sceneTrans.Progress < 1.0f)
			{
				// 画面のロード進捗へProgress値を渡す
				screen.SetProgress(this.sceneTrans.Progress);
				yield return null;
			}
			screen.SetProgress(this.sceneTrans.Progress);

			// Unityシーンの切り替え終了まで待つ
			while (this.sceneTrans.IsDone == false)
			{
				yield return null;
			}

			// 少しだけWait
			yield return new WaitForSeconds(0.5f);

			// 画面の遷移表現開始
			screen.HideScreen();

			// 画面の遷移表現が終わるまで待つ
			while (screen.IsFading())
			{
				yield return null;
			}

			// SceneStateの遷移、移動先のステートスタート
			mediator.SetState(nextScene);
		}
	}
}