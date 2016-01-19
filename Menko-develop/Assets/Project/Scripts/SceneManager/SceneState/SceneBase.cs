using UnityEngine;
using System.Collections;
using AppUtils;
using AppUtils.Assets;

namespace MenkoiMonster.Scene
{
	public abstract class SceneBase : IState<SceneName>
	{
		/// <summary>
		/// このシーンに使用されるシーン名を取得する
		/// </summary>
		/// <returns>The load scene names.</returns>
		public abstract string[] GetLoadSceneNames();

		/// <summary>
		/// このシーンで使用されるアセットパス群を取得する
		/// </summary>
		/// <returns>The asset load paths.</returns>
		public abstract AssetLoadPath[] GetAssetLoadPaths();

		public abstract void Init(StateMediator<SceneName> mediator);

		public abstract void Update(StateMediator<SceneName> mediator);

		public abstract void Exit(StateMediator<SceneName> mediator);

		public abstract SceneName GetKey();
	}
}