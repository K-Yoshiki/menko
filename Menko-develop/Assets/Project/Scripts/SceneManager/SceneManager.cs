using UnityEngine;
using MenkoiMonster.Scene;
using AppUtils;

namespace MenkoiMonster
{
	public class SceneManager : UnitySingleton<SceneManager>
	{
		StateMachine<SceneName> stateMachine;

		protected override void Initialize()
		{
			DontDestroyOnLoad(this.gameObject);
			stateMachine = new StateMachine<SceneName>();
		}

		public void Init(IState<SceneName> sceneState)
		{
			stateMachine.SetState(sceneState);
		}

		public void ChangeScene(SceneBase sceneState)
		{
			var sceneNames = sceneState.GetLoadSceneNames();
			var preLoadPaths = sceneState.GetAssetLoadPaths();
			var transition = new SceneTransition(sceneNames, preLoadPaths);
			var loading = new LoadingScene(sceneState, transition);
			stateMachine.SetState(loading);
		}

		public SceneName CurrentScene
		{
			get { return stateMachine.CurrentKey; }
		}

		void Update()
		{
			stateMachine.UpdateState();
		}
	}

	public enum SceneName
	{
		None,
		Title,
		Home,
		Battle,
		Loading,
	}
}