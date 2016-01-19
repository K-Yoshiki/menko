using System;
using UnityEngine;
using UnityObject = UnityEngine.Object;

namespace AppUtils.Assets
{
	public class Asset
	{
		readonly string path;
		readonly UnityObject asset;
		readonly ResourceRequest request;
		readonly Action<Asset> unloaded;

		public Asset(UnityObject asset, string path, Action<Asset> unloaded)
		{
			this.path = path;
			this.asset = asset;
			this.unloaded = unloaded;
			this.request = null;
		}

		public Asset(ResourceRequest request, string path, Action<Asset> unloaded)
		{
			this.path = path;
			this.request = request;
			this.unloaded = unloaded;
			this.asset = null;
		}

		public void Unload()
		{
			UnloadAsset();
			unloaded(this);
		}

		public UnityObject Data
		{
			get
			{
				if (!IsAsync)
					return this.asset;
				if (IsLoadComplated)
					return this.request.asset;
				return null;
			}
		}

		public bool IsAsync
		{
			get { return this.request != null; }
		}

		public bool IsLoadComplated
		{
			get
			{
				if (IsAsync)
				{
					return this.request.isDone;
				}
				return true;
			}
		}

		public float LoadProgress
		{
			get
			{
				if (IsAsync)
					return this.request.progress;
				return 1.0f;
			}
		}

		public string Path
		{
			get { return this.path; }
		}

		void UnloadAsset()
		{
			if (IsAsync)
			{
				Resources.UnloadAsset(this.request.asset);
				return;
			}
			Resources.UnloadAsset(this.asset);
		}
	}
}