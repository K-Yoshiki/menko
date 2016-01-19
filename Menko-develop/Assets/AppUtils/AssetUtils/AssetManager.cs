using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AppUtils.Assets
{
	public struct AssetLoadPath
	{
		public string path;
		public System.Type type;

		public AssetLoadPath(string path)
		{
			this.path = path;
			type = typeof(Object);
		}

		public AssetLoadPath(string path, System.Type type)
		{
			this.path = path;
			this.type = type;
		}
	}

	public static class AssetManager
	{
		static Dictionary<string, Asset> loadedAssets;

		static AssetManager()
		{
			loadedAssets = new Dictionary<string, Asset>();
		}

		public static AssetData Load(AssetLoadPath loadPath, bool isAsync = false)
		{
			return Load(loadPath.path, loadPath.type, isAsync);
		}

		public static AssetData Load(string path, bool isAsync = false)
		{
			return Load(path, typeof(Object), isAsync);
		}

		public static AssetData Load<T>(string path, bool isAsync = false) where T : Object
		{
			return Load(path, typeof(T), isAsync);
		}

		public static AssetData Load(string path, System.Type type, bool isAsync = false)
		{
			Asset asset;
			if (loadedAssets.TryGetValue(path, out asset))
			{
				return new AssetData(asset);
			}

			if (isAsync == false)
			{
				asset = LoadResource(path, type);
			}
			else
			{
				asset = LoadResourceAsync(path, type);
			}

			return new AssetData(asset);
		}

		static Asset LoadResource(string path, System.Type type)
		{
			Asset asset = null;
			var obj = Resources.Load(path, type);
			if (obj != null)
			{
				asset = new Asset(obj, path, OnUnloaded);
				loadedAssets[path] = asset;
			}
			return asset;
		}

		static Asset LoadResourceAsync(string path, System.Type type)
		{
			Asset asset = null;
			ResourceRequest request = Resources.LoadAsync(path, type);
			if (request != null)
			{
				asset = new Asset(request, path, OnUnloaded);
				loadedAssets[path] = asset;
			}
			return asset;
		}

		static void OnUnloaded(Asset asset)
		{
			loadedAssets.Remove(asset.Path);
		}

		public static void UnloadAll()
		{
			foreach (var asset in loadedAssets.Values)
			{
				asset.Unload();
			}
			loadedAssets.Clear();
		}
	}
}