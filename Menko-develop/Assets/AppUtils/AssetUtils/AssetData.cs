using UnityEngine;

namespace AppUtils.Assets
{
	public struct AssetData
	{
		readonly Asset asset;

		public AssetData(Asset asset)
		{
			this.asset = asset;
		}

		public bool IsNull
		{
			get { return this.asset == null; }
		}

		public Object Asset
		{
			get { return this.asset.Data; }
		}

		public bool IsLoadCompleted
		{
			get { return this.asset.IsLoadComplated; }
		}

		public float LoadProgress
		{
			get { return this.asset.LoadProgress; }
		}

		public void Remove()
		{
			this.asset.Unload();
		}
	}
}