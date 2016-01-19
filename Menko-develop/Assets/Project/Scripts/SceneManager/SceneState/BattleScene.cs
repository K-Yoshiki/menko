using UnityEngine;
using AppUtils;
using AppUtils.Assets;
using MenkoiMonster.Battle;
using MenkoiMonster.Battle.State;

namespace MenkoiMonster.Scene
{
	public class BattleScene : SceneBase
	{
		BattleStateMachine stateMachine;
		BattleManager manager;
		BattleData data;

		public BattleScene(BattleData setup)
		{
			data = setup;
			manager = new BattleManager(setup);
			this.stateMachine = new BattleStateMachine(manager);
		}

		public override void Init(StateMediator<SceneName> mediator)
		{
			Debug.Log("Start Battle Scene");

			manager.Init();
			this.stateMachine.Init();
		}

		public override void Update(StateMediator<SceneName> mediator)
		{
			this.stateMachine.Update();

			if (this.manager.IsBattleEnd())
			{
				SceneManager.Instance.ChangeScene(new HomeScene());
			}
		}

		public override void Exit(StateMediator<SceneName> mediator)
		{
			
		}

		public override SceneName GetKey()
		{
			return SceneName.Battle;
		}

		public override string[] GetLoadSceneNames()
		{
			return new string[]{ "Battle", "BattleUI" };
		}

		public override AssetLoadPath[] GetAssetLoadPaths()
		{
			return data.GetPreLoadPaths();
		}
	}
}