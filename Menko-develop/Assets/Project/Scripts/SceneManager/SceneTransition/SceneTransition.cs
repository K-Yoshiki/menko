using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AppUtils;
using AppUtils.Assets;

namespace MenkoiMonster.Scene
{
	public class SceneTransition
	{
		SceneAsyncLoader sceneLoader;
		AssetAsyncLoader assetLoader;

		public SceneTransition(string[] loadSceneNames, AssetLoadPath[] loadAssetPaths)
		{
			sceneLoader = new SceneAsyncLoader(loadSceneNames);
			assetLoader = new AssetAsyncLoader(loadAssetPaths);
		}

		public void Load()
		{
			sceneLoader.LoadStart();
			assetLoader.LoadStart();
		}

		public float Progress
		{
			get
			{
				if (assetLoader.IsNoneAsset())
				{
					return SceneLoadProgress;
				}
				return (SceneLoadProgress + AssetLoadProgress) / 2.0f;
			}
		}

		public bool IsDone
		{
			get { return sceneLoader.IsDone(); }
		}

		public float SceneLoadProgress
		{
			get { return sceneLoader.Progress(); }
		}

		public float AssetLoadProgress
		{
			get { return assetLoader.Progress(); }
		}
	}

	public class SceneAsyncLoader
	{
		string[] loadSceneNames;
		List<AsyncOperation> operations;
		int loadCount;
		bool isLoading;

		public SceneAsyncLoader(string[] loadSceneNames)
		{
			operations = new List<AsyncOperation>();
			this.loadSceneNames = loadSceneNames;
			loadCount = this.loadSceneNames.Length;
		}

		public void LoadStart()
		{
			if (isLoading)
			{
				return;
			}
			isLoading = true;
			SceneManager.Instance.StartCoroutine(Load());
		}

		public void Transit()
		{
			operations.ForEach(o => o.allowSceneActivation = true);
		}

		public bool IsDone()
		{
			return operations.TrueForAll(o => o.isDone);
		}

		public float Progress()
		{
			return (operations.Sum(o => o.progress) / loadCount);
		}

		AsyncOperation LoadAsync(string sceneName)
		{
			AsyncOperation operation = Application.LoadLevelAsync(sceneName);
			if (operation != null)
			{
				operations.Add(operation);
			}
			return operation;
		}

		AsyncOperation LoadAddativeAsync(string sceneName)
		{
			AsyncOperation operation = Application.LoadLevelAdditiveAsync(sceneName);
			if (operation != null)
			{
				operations.Add(operation);
			}
			return operation;
		}

		IEnumerator Load()
		{
			int index = 0;

			// まず通常のシーンロード
			var operation = LoadAsync(loadSceneNames[index]);
			if (operation == null)
			{
				yield break;
			}

			// 終わるまで待つ
			while (operation.isDone)
			{
				yield return null;
			}

			// 追加シーンがあるなら読み込み開始
			for (index = 1; index < loadSceneNames.Length; ++index)
			{
				var addOperate = LoadAddativeAsync(loadSceneNames[index]);
				if (addOperate == null)
				{
					--loadCount;
					continue;
				}

				// 終わるまで待つ
				while (addOperate.isDone)
				{
					yield return null;
				}
			}
		}
	}

	public class AssetAsyncLoader
	{
		AssetLoadPath[] loadAssetPaths;
		List<AssetData> assetLoads;

		public AssetAsyncLoader(AssetLoadPath[] loadAssetPaths)
		{
			this.assetLoads = new List<AssetData>();
			this.loadAssetPaths = loadAssetPaths;
		}

		public void LoadStart()
		{
			LoadAssets();
		}

		public bool IsNoneAsset()
		{
			return assetLoads.Count <= 0;
		}

		public float Progress()
		{
			if (IsNoneAsset())
			{
				return 1.0f;
			}
			return assetLoads.Sum(a => a.LoadProgress) / assetLoads.Count;
		}

		void LoadAssets()
		{
			for (int i = 0; i < loadAssetPaths.Length; ++i)
			{
				AssetData loadData = AssetManager.Load(loadAssetPaths[i], true);

				// Asset Not Found.
				if (loadData.IsNull)
				{
					continue;
				}
				assetLoads.Add(loadData);
			}
		}
	}
}